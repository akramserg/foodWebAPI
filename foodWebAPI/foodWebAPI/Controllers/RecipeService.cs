using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace foodWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeService: ControllerBase
    {
		public ILogger<RecipeService> _logger;
		public RecipeService(ILogger<RecipeService> logger)
		{
			_logger = logger;
		}

		public record Recipe
        {
			public string Id = null!;
			public string Title = "";
			public List<string> Components = new();
		};

		[HttpGet()]
		[SwaggerOperation(Summary = "Get All Recipes")]
        [SwaggerResponse(200, "Entries found", typeof(Recipe[]))]
        [SwaggerResponse(404, "Entries not found")]
        [SwaggerResponse(400, "Bad request")]
        public IActionResult GetFood()
		{
			return Ok();
		}
	}
}

