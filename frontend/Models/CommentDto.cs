using System.Text.Json.Serialization;

namespace frontend.Models
{
	public class CommentDto
	{
		public int CommentId { get; set; }

		public int? ParentCommentId { get; set; }
		public CommentDto? ParentComment { get; set; }

		[JsonIgnore]
		public List<CommentDto> Replies { get; set; } = new();

		// public string AuthorName { get; set; }
		public Author? Author { get; set; }

		public string? BodyHtml { get; set; } = string.Empty;
		public string? BodySource { get; set; } = string.Empty;

		public int ClapsCount { get; set; }
		public int RepliesCount { get; set; }

		//public int ArticleId { get; set; }
		//public Article? Article { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public bool IsEdited { get; set; }
		public bool IsDeleted { get; set; }
		public bool IsPinned { get; set; }
	}


	//public class CommentDto
	//{
	//	public int commentId { get; set; }
	//	public object parentCommentId { get; set; }
	//	public object parentComment { get; set; }
	//	public Author author { get; set; }
	//	public string bodyHtml { get; set; }
	//	public string bodySource { get; set; }
	//	public int clapsCount { get; set; }
	//	public int repliesCount { get; set; }
	//	public DateTime createdAt { get; set; }
	//	public DateTime updatedAt { get; set; }
	//	public bool isEdited { get; set; }
	//	public bool isDeleted { get; set; }
	//	public bool isPinned { get; set; }
	//}

	//public class Author
	//{
	//	public int userId { get; set; }
	//	public string username { get; set; }
	//	public object fullName { get; set; }
	//	public string email { get; set; }
	//	public object bio { get; set; }
	//	public object profilePic { get; set; }
	//	public object url { get; set; }
	//}

}
