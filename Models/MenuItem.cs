using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
   public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public MenuCategory Category { get; set; }
        public bool IsAvailable { get; set; }
        public string? ImageUrl { get; set; }
        public int? RecipeId { get; set; }
        
        // Navigation properties
        public virtual Recipe? Recipe { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public enum MenuCategory
    {
        Appetizer,
        MainCourse,
        Dessert,
        Beverage,
        Side
    }
}