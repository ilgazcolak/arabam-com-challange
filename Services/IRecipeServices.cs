using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arabam.Com.Services
{
    public interface IRecipeServices
    {
        ActionResult GetRecipes([FromBody] JObject postData);
        ActionResult GetCategories();
        ActionResult AddRecipe([FromBody] JObject postData);
    }
}
