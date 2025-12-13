using ThoughtHub.Api.Models.Content;

namespace ThoughtHub.Api.Models.Editor
{
	public class ArticleEditModel
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public bool IsScheduled { get; set; }

		// TODO: ImageField

		public string Slug { get; set; }

		public string MetaTitle { get; set; }

		public string OgTitle { get; set; }

		public string Published { get; set; }

		public int CommentCount { get; set; }

		public IList<BlockEditModel> Blocks { get; set; } = new List<BlockEditModel>();
		// TODO: Add selected tags
		// TODO: Add tags

		public StatusMessage Status { get; set; }

		public int AuthorProfileId { get; set; }
	}

	public class StatusMessage
	{
		public string Type { get; set; }

		public string Body { get; set; }
	}
}
