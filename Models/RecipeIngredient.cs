using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
  public class RecipeIngredient
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty; // cups, tablespoons, grams, etc.
        public bool IsOptional { get; set; }
        
        // Navigation property
        public virtual Recipe Recipe { get; set; } = null!;
    }
}