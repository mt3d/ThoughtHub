using ThoughtHub.Api.Core.Entities.ReadingList;

namespace ThoughtHub.Services
{
	public interface IReadingListService
	{
		Task<ReadingList> CreateCustomAsync(
			Guid ownerId,
			string name,
			string? description,
			CancellationToken ct);

		Task<ReadingList?> GetBySlugAsync(
			Guid ownerId,
			string slug,
			Guid? viewerId,
			CancellationToken ct);

		Task EnsureDefaultReadingListAsync(Guid profileId);

		Task AddArticleToReadingList(Guid profileId, Guid articleId);
	}
}
