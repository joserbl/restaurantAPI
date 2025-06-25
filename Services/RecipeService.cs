using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly RestaurantContext _context;

        public RecipeService(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<List<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.Include(r => r.Ingredients).ToListAsync();
        }

        public async Task<Recipe?> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes.Include(r => r.Ingredients)
                                         .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Recipe>> GetRecipesByDifficultyAsync(DifficultyLevel difficulty)
        {
            return await _context.Recipes.Where(r => r.Difficulty == difficulty)
                                         .Include(r => r.Ingredients)
                                         .ToListAsync();
        }
    }
}