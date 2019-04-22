using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arabam.Com.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Arabam.Com.Services
{
    public class RecipeServices : ControllerBase, IRecipeServices
    {

        private readonly RecipeContext _context;
        public RecipeServices(RecipeContext context)
        {
            _context = context;

        }

        //
        public ActionResult GetRecipes([FromBody] JObject postData)
        {
            var start = postData["start"];
            var end = postData["end"];

            try
            {
                if (_context.Recipes.ToList().Count() == 0)
                {
                    return StatusCode(204, new { error = "No recipes found" });
                }

                if (start != null && end != null)
                {
                    if (int.TryParse(start.ToString(), out int value) && int.TryParse(end.ToString(), out int value2))
                    {
                        var total = _context.Recipes.ToList().Count();
                        start = (int)start < 0 ? 0 : start;
                        end = (int)end > total - 1 ? total - 1 : end;
                        var count = (int)end - (int)start + 1;

                        var recipes = _context.Recipes.ToList();
                        List<Recipe> filteredRecipes = new List<Recipe>();

                        var index = 0;
                        foreach (Recipe r in recipes)
                        {
                            if (index >= (int)start && index <= (int)end)
                                filteredRecipes.Add(r);
                            index++;
                        }

                        return Ok(new { results = count, total = _context.Recipes.Count(), recipes = filteredRecipes });
                    }
                    else
                    {
                        return BadRequest(new { error = "start and end params should be integer" });
                    }
                }
                else
                {
                    return Ok(new { total = _context.Recipes.Count(), _context.Recipes });
                }
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal error occurred" });
            }
        }

        public ActionResult GetCategories()
        {
            var recipes = _context.Recipes.ToList();
            List<string> categories = new List<string>();

            foreach (Recipe r in recipes)
            {
                Console.WriteLine("category" + r.Categories);
                foreach (string c in r.Categories)
                {
                    if (!categories.Contains(c))
                    {
                        categories.Add(c);
                    }
                }
            }
            if (categories.Count > 0)
            {
                return Ok(new { results = categories.Count(), categories });
            }
            else if (categories.Count == 0)
            {
                return StatusCode(204, new { error = "No categories found" });
            }

            return StatusCode(500, new { error = "Internal error occurred" });
        }

        public ActionResult AddRecipe([FromBody] JObject postData)
        {
            try
            {

                Recipe duplicate = null;

                var title = postData["title"] ?? null;
                var categories = postData["categories"].ToObject<string[]>() ?? null;
                var directions = postData["directions"]["step"] ?? null;
                var ingredients = postData["ingredients"].ToObject<JObject[]>() ?? null;

                if (title == null || categories == null)
                {
                    return StatusCode(400, new { error = "Wrong JSON object" });
                }

                duplicate = _context.Recipes.FirstOrDefault(x => x.Title == title.ToString());

                if (duplicate != null)
                {
                    return StatusCode(409, new { error = "Recipe duplicated" });
                }

                var recipe = new Recipe()
                {
                    Title = title.ToString(),
                    Categories = categories,
                    Ingredients = ingredients,
                    Directions = directions.ToString()
                };


                _context.Recipes.Add(recipe);
                _context.SaveChanges();

                return StatusCode(201, new { success = "Recipe created" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Internal error occurred" });
            }
        }
    }
}
