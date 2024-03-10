using System;
namespace foodWebAPI.Models
{
	public class Ingredient
	{
		public string Id { get; set; } = null!;
		public string Title { get; set; } = null!;
        public string? Note { get; set; } = null!;
    }
}

