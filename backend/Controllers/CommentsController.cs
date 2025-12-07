using ThoughtHub.Data;
using ThoughtHub.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ThoughtHub.Controllers
{
	public class CommentsController : ControllerBase
	{
		private PlatformContext context;
		//private IUserAccessor userAccessor;

		public CommentsController(PlatformContext context)//, IUserAccessor userAccessor)
		{
			this.context = context;
			//this.userAccessor = userAccessor;
		}

		[HttpPost("articles/{slug}/comments")]
		[Authorize]
		public async Task<IActionResult> Create(string slug, [FromBody] string bodySource)
		{
			Article? article = await context.Articles
				.Include(a => a.Comments)
				.FirstOrDefaultAsync(a => a.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			Profile author = await context.Profiles.FirstAsync(u => u.User.UserName == User.Identity!.Name);

			Comment comment = new Comment
			{
				Author = author,
				Article = article,
				BodySource = bodySource,
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow
			};

			await context.Comments.AddAsync(comment);
			await context.SaveChangesAsync();

			return Ok(comment);
		}

		[HttpGet("articles/{slug}/comments")]
		public async Task<IActionResult> Get(string slug)
		{
			Article? article = await context
				.Articles.Include(a => a.Comments).ThenInclude(c => c.Author)
				.FirstOrDefaultAsync(a => a.Slug == slug);

			if (article == null)
			{
				return NotFound();
			}

			return Ok(article.Comments);
		}

		[HttpDelete("articles/{slug}/comments/{id}")]
		[Authorize]
		public async Task<IActionResult> Delete(string slug, int id)
		{
			Article? article = await context
				.Articles.Include(a => a.Comments)
				.FirstOrDefaultAsync(a => a.Slug == slug);

			Comment? comment = article?.Comments.FirstOrDefault(c => c.CommentId == id);

			if (comment == null)
			{
				return NotFound();
			}

			// context.Comments.Remove(comment);
			comment.IsDeleted = true;

			await context.SaveChangesAsync();

			return Ok();
		}

		/// <summary>
		/// Returns a comment with its replies.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("/comments/{id}")]
		public async Task<IActionResult> Get(int id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Updates the markdown of a single comment.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		[HttpPut("/comments/{id}")]
		[Authorize]
		public async Task<IActionResult> Update(int id, [FromBody] string bodySource)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Increase the claps count for a comment.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		[HttpPost("/comments/{id}/clap")]
		[Authorize]
		public async Task<IActionResult> Clap(int id, [FromBody] int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reports a comment for modertaion purposes.
		/// </summary>
		/// <param name="id"></param>
		/// <param name="reason"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		[HttpPost("/comments/{id}/report")]
		[Authorize]
		public async Task<IActionResult> Report(int id, [FromBody] string reason)
		{
			throw new NotImplementedException();
		}
	}
}
