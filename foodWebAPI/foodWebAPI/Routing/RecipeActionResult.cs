using System;
using Microsoft.AspNetCore.Mvc;

namespace foodWebAPI.Routing
{
	public class RecipeActionResult<T>
	{
		public string? Error;
		public T? Result;
	}
}

