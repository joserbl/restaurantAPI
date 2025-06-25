using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRecipeService
    {
        Task<List<Recipe>> GetAllRecipesAsync();
        Task<Recipe?> GetRecipeByIdAsync(int id);
        Task<List<Recipe>> GetRecipesByDifficultyAsync(DifficultyLevel difficulty);
    }
}