using System;
using foodWebAPI.DB;
using foodWebAPI.Models;
using foodWebAPI.Routing;
using Microsoft.EntityFrameworkCore;

namespace foodWebAPI.Services
{
	public class RecipeService : IRecipeService
	{
        public ILogger<RecipeService> _logger;
        public DataContext _dbContext;

        public RecipeService(ILogger<RecipeService> logger,
                        DataContext dbContext)

        {
            _logger = logger;
            _dbContext = dbContext;
        }

		public async Task<RecipeActionResult<IEnumerable<Recipe>>> GetRecipesAsync(int page, CancellationToken cancellationToken)
		{
            var offset = page * 10;
            var recipes = await _dbContext.Recipes
                .OrderBy(b => b.Id)
                .Skip(offset)
                .ToListAsync(cancellationToken);

            return new RecipeActionResult<IEnumerable<Recipe>>
            {
                Error = null,
                Result = recipes
            };
        }

        public async Task<RecipeActionResult<Recipe?>> GetRecipeAsync(string recipeId, CancellationToken cancellationToken)
        {
            var recipe = await _dbContext.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);

            return new RecipeActionResult<Recipe?>
            {
                Error = null,
                Result = recipe
            };
        }
    }
}

