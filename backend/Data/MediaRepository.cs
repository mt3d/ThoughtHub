using Microsoft.EntityFrameworkCore;
using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Data
{
	public class MediaRepository : IMediaRepository
	{
		private readonly PlatformContext _context;

		public MediaRepository(PlatformContext context)
		{
			_context = context;
		}

		// Save
		public async Task AddOrUpdate(Image model)
		{
			var image = await _context.Images
				.Include(i => i.Versions)
				.FirstOrDefaultAsync(i => i.Id == model.Id)
				.ConfigureAwait(false);

			if (image is null)
			{
				image = new Image
				{
					CreatedAt = DateTime.Now,
				};

				await _context.Images.AddAsync(image);
			}

			image.Filename = model.Filename;
			image.FolderId = model.FolderId;

			var currentVersionsIds = model.Versions.Select(v => v.Id).ToList();
			var removedVersions = image.Versions.Where(v => !currentVersionsIds.Contains(v.Id)).ToArray();

			if (removedVersions.Any())
			{
				_context.ImageVersions.RemoveRange(removedVersions);
			}

			foreach (var version in model.Versions)
			{
				// TODO: Add only new versions
			}

			await _context.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}
