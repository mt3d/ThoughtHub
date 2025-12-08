namespace ThoughtHub.Api.Models.Content
{
	public class ArticleEditModel : ContentEditModel
	{
		// TODO: ImageField

		public string Slug { get; set; }

		public string MetaTitle { get; set; }

		public string OgTitle { get; set; }

		public string Published { get; set; }

		public int CommentCount { get; set; }

		// TODO: Add selected tags
		// TODO: Add tags
	}
}
