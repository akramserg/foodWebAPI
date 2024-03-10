using System;
namespace foodWebAPI.Models
{
	public class Recipe
	{
		public string Id { get; set; } = null!;
		public string Title { get; set; } = null!;
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}

