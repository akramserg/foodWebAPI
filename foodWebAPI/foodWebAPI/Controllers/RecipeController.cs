using System;
using System.Linq;
using System.Threading;
using foodWebAPI.DB;
using foodWebAPI.Models;
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

        public record GetRecipesRequeset(Pagination? Pagination);
		public record GetRecipesResponse(List<Recipe>? Recipes = null);

		[HttpGet()]
		[SwaggerOperation(Summary = "Get All Recipes")]
        [SwaggerResponse(200, "Entries found", typeof(Recipe[]))]
        [SwaggerResponse(404, "Entries not found")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> GetRecipes([FromQuery] GetRecipesRequeset request, CancellationToken cancellationToken)
		{
            var result = await _recipeService.GetRecipesAsync(request.Pagination != null ? request.Pagination.Page : 1, cancellationToken);
            if (result.Error != null)
            {
                _logger.LogError("Error", result.Error);
                return BadRequest();
            }
            return Ok(new GetRecipesResponse(result.Result?.ToList()));
		}

        public record GetRecipeResponse(Recipe? Recipe);

        [HttpGet("{recipeId}")]
        [SwaggerOperation(Summary = "Get All Recipes")]
        [SwaggerResponse(200, "Entries found", typeof(Recipe[]))]
        [SwaggerResponse(404, "Entries not found")]
        [SwaggerResponse(400, "Bad request")]
        public async Task<IActionResult> GetRecipe(string recipeId, CancellationToken cancellationToken)
        {
            var result = await _recipeService.GetRecipeAsync(recipeId, cancellationToken);
            if (result.Error != null)
            {
                _logger.LogError("Error", result.Error);
                return BadRequest();
            }
            return Ok(new GetRecipeResponse(result.Result));
        }
    }
}

