using ThoughtHub.Data.Entities.Media;

namespace ThoughtHub.Data
{
	public interface IMediaRepository
	{
		Task AddOrUpdate(Image model);
	}
}
