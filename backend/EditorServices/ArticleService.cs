using ThoughtHub.Api.Models.Editor;

namespace ThoughtHub.EditorServices
{
	public class ArticleService
	{
		public async Task<ArticleEditModel> GetById(Guid id)
		{
			throw new NotImplementedException();
		}

		// create or update
		public async Task Save(ArticleEditModel model, bool draft)
		{
			if (model.Id == Guid.Empty)
			{
				model.Id = Guid.NewGuid();
			}

			throw new NotImplementedException();
		}
	}
}
