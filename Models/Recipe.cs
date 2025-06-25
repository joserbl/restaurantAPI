using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int PreparationTimeMinutes { get; set; }
        public int ServingSize { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        
        // Navigation properties
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard,
        Expert
    }
}