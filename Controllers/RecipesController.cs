using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> GetRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null) return NotFound();

            return Ok(recipe);
        }
        
         [HttpGet("difficulty/{difficulty}")]
        public async Task<ActionResult<List<Recipe>>> GetRecipesByDifficulty(DifficultyLevel difficulty)
        {
            var recipes = await _recipeService.GetRecipesByDifficultyAsync(difficulty);
            return Ok(recipes);
        }


    }
}