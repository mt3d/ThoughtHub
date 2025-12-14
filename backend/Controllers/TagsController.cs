using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThoughtHub.Api.Models;
using ThoughtHub.Data;

namespace ThoughtHub.Controllers
{
	[Route("api/tags")]
	[ApiController]
	public class TagsController : ControllerBase
	{
		private readonly PlatformContext context;
		private readonly IMapper mapper;

		public TagsController(PlatformContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var tags = await context.Tags
				.OrderBy(t => t.Name)
				.AsNoTracking()
				.ToListAsync();

			return Ok(mapper.Map<List<TagModel>>(tags));
		}

		[HttpGet("{topic-slug}/articles")]
		public async Task<IActionResult> GetArticlesByTopic(string topic, [FromQuery] int limit, [FromQuery] int offset = 0)
		{
			throw new NotImplementedException();
		}
	}
}
