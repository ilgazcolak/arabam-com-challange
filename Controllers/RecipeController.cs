using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Arabam.Com.Models;
using Arabam.Com.Services;

namespace ArabamCom.Controllers
{
    [Route("/services/recipe/")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeServices _services;

        public RecipeController(IRecipeServices services)
        {
            _services = services;
        }

        // 1
        // URL: /services/recipe/all 
        // HTTP Method: POST
        // Params(optional): (int)start: first index , (int)end: last index 
        [HttpPost]
        [Produces("application/json")]
        [Route("all")]
        public ActionResult GetRecipes([FromBody] JObject postData)
        {
            return _services.GetRecipes(postData);
        }
        // 2
        // URL: /services/recipe/filter/categories
        // HTTP Method: GET
        [HttpGet]
        [Route("filter/categories")]
        public ActionResult GetCategories()
        {
            return _services.GetCategories();
        }

        // 3
        // URL: /services/recipe/add
        // HTTP Method: PUT
        [HttpPut]
        [Produces("application/json")]
        [Route("add")]
        public ActionResult AddRecipe([FromBody] JObject postData)
        {
            return _services.AddRecipe(postData);
        }
    }
}
