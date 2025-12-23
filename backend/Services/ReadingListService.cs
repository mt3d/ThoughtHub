using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Core.Entities.ReadingList;
using ThoughtHub.Data;

namespace ThoughtHub.Services
{
	public class ReadingListService
	{
		private readonly PlatformContext _context;

		public ReadingListService(PlatformContext context)
		{
			_context = context;
		}

		public async Task<ReadingList> CreateCustomAsync(
			Guid ownerId,
			string name,
			string? description,
			CancellationToken ct)
		{
			// Two-phase slug generation.
			var baseSlug = Utils.GenerateSlug(name);
			var slug = await EnsureUniqueSlugAsync(ownerId, baseSlug, ct);

			var list = ReadingList.CreateCustom(ownerId, name, slug);
			list.UpdateMetadata(name, description);

			_context.ReadingLists.Add(list);
			await _context.SaveChangesAsync(ct);

			return list;
		}

		public async Task<ReadingList?> GetBySlugAsync(
			Guid ownerId,
			string slug,
			Guid? viewerId,
			CancellationToken ct)
		{
			var query = _context.ReadingLists.Where(l => l.OwnerId == ownerId && l.Slug == slug);

			if (viewerId != ownerId)
			{
				query = query.Where(l => l.Visibility == ReadingListVisibility.Public);
			}

			return await query.FirstOrDefaultAsync(ct);
		}

		/// <summary>
		/// Find the smallest suffix that produces a slug not yet used by the same owner.
		/// </summary>
		/// <param name="baseSlug"></param>
		/// <param name="ownerId"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		private async Task<string> EnsureUniqueSlugAsync(
			Guid ownerId,
			string baseSlug,
			CancellationToken ct)
		{
			var existingSlugs = await _context.ReadingLists
				.Where(l => l.OwnerId == ownerId && (l.Slug == baseSlug || l.Slug.StartsWith(baseSlug + "-")))
				.Select(l => l.Slug)
				.ToListAsync(ct);

			if (existingSlugs.Count == 0)
			{
				return baseSlug;
			}

			var maxSuffix = 1;

			foreach (var slug in existingSlugs)
			{
				// Skip the first slug.
				if (slug.Length == baseSlug.Length)
				{
					continue;
				}

				// Any other slug should have a numerical suffix.
				var suffixPart = slug.Substring(baseSlug.Length + 1);

				if (int.TryParse(suffixPart, out var suffix))
				{
					if (suffix >= maxSuffix)
					{
						maxSuffix = suffix + 1;
					}
				}
			}

			return $"{baseSlug}-{maxSuffix}";
		}
	}
}
