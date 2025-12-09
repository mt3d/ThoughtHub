using Microsoft.AspNetCore.Mvc;
using ThoughtHub.EditorServices;

namespace ThoughtHub.Controllers
{
	[Route("content")]
	[ApiController]
	public class ContentController : Controller
	{
		private readonly ContentTypeService _contentTypeService;

		public ContentController(ContentTypeService contentTypeService)
		{
			_contentTypeService = contentTypeService;
		}

		[Route("block/{type}")]
		public async Task<IActionResult> CreateBlockAsync(string type)
		{
			var block = await _contentTypeService.CreateBlockAsync(type);

			if (block != null)
			{
				return Ok(block);
			}

			return NotFound();
		}
	}
}
