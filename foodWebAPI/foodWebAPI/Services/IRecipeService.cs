using System;
using foodWebAPI.Models;
using foodWebAPI.Routing;

namespace foodWebAPI.Services;

public interface IRecipeService
{
    public Task<RecipeActionResult<IEnumerable<Recipe>>> GetRecipesAsync(int page, CancellationToken cancellationToken);
    public Task<RecipeActionResult<Recipe?>> GetRecipeAsync(string recipeId, CancellationToken cancellationToken);
}


