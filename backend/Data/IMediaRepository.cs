using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Data
{
	public interface IMediaRepository
	{
		Task<Image> GetById(Guid id);

		Task AddOrUpdate(Image model);
	}
}
