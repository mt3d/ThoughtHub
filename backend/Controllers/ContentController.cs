using Microsoft.AspNetCore.Mvc;

namespace ThoughtHub.Controllers
{
	[Route("content")]
	[ApiController]
	public class ContentController : Controller
	{
		[Route("block/{type}")]
		public async Task<IActionResult> CreateBlockAsync(string type)
		{
			return NotFound();
		}
	}
}
