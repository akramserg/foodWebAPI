using System;
using System.Linq;
using foodWebAPI.DB;
using foodWebAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace foodWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController: ControllerBase
    {
		public ILogger<RecipeController> _logger;
        public IRecipeService _recipeService;

		public RecipeController(
			ILogger<RecipeController> logger,
            IRecipeService recipeService)
		{
			_logger = logger;
            _recipeService = recipeService;
		}

		public record Recipe
        {
			public string Id = null!;
			public string Title = "";
			public List<string> Components = new();
		};

        public record Pagination
        {
            private int _page = 1;
            public int Page {
                get
                {
                    return _page;
                }
                set
                {
                    if (value > 0)
                    {
                        _page = value;
                    }
                    else
                    {
                        _page = 1;
                    }
                }
            }
        }

        public record GetRecipeRequese(Pagination? Pagination);
		public record GetRecipeResponse(List<Recipe>? Recipe = default);

		[HttpGet()]
		[SwaggerOperation(Summary = "Get All Recipes")]
        [SwaggerResponse(200, "Entries found", typeof(Recipe[]))]
        [SwaggerResponse(404, "Entries not found")]
        [SwaggerResponse(400, "Bad request")]
        public IActionResult GetRecipes([FromQuery] GetRecipeRequese request, CancellationToken cancellationToken)
		{
            var result = _recipeService.GetRecipesAsync(request.Pagination != null ? request.Pagination.Page : 1, cancellationToken);
            return Ok(new GetRecipeResponse());
		}

        [HttpGet("{recipeId}")]
        [SwaggerOperation(Summary = "Get All Recipes")]
        [SwaggerResponse(200, "Entries found", typeof(Recipe[]))]
        [SwaggerResponse(404, "Entries not found")]
        [SwaggerResponse(400, "Bad request")]
        public IActionResult GetRecipe([FromQuery] GetRecipeRequese request)
        {
            return Ok(new GetRecipeResponse());
        }
    }
}

