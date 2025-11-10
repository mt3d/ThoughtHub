using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Data
{
	public interface IMediaRepository
	{
		Task<Image> GetById(int id);

		Task AddOrUpdate(Image model);
	}
}
