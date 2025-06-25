using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class DataSeeder
    {
        public static void SeedData(RestaurantContext context)
        {
            if (context.Users.Any()) return; // Already seeded

            // Seed Users
            var users = new[]
            {
                new User { Username = "admin", Email = "admin@restaurant.com", PasswordHash = "hashed_password", Role = UserRole.Admin, CreatedAt = DateTime.UtcNow },
                new User { Username = "staff", Email = "staff@restaurant.com", PasswordHash = "hashed_password", Role = UserRole.Staff, CreatedAt = DateTime.UtcNow },
                new User { Username = "customer", Email = "customer@email.com", PasswordHash = "hashed_password", Role = UserRole.Customer, CreatedAt = DateTime.UtcNow }
            };
            context.Users.AddRange(users);

            // Seed Recipes
            var recipes = new[]
            {
                new Recipe { Name = "Classic Margherita Pizza", Description = "Traditional Italian pizza", Instructions = "Make dough, add sauce, cheese, basil", PreparationTimeMinutes = 45, ServingSize = 4, Difficulty = DifficultyLevel.Medium },
                new Recipe { Name = "Caesar Salad", Description = "Fresh romaine lettuce salad", Instructions = "Mix lettuce, dressing, croutons, parmesan", PreparationTimeMinutes = 15, ServingSize = 2, Difficulty = DifficultyLevel.Easy }
            };
            context.Recipes.AddRange(recipes);
            context.SaveChanges();

            // Seed Recipe Ingredients
            var ingredients = new[]
            {
                new RecipeIngredient { RecipeId = 1, Name = "Pizza Dough", Quantity = 1, Unit = "ball", IsOptional = false },
                new RecipeIngredient { RecipeId = 1, Name = "Tomato Sauce", Quantity = 0.5m, Unit = "cup", IsOptional = false },
                new RecipeIngredient { RecipeId = 1, Name = "Mozzarella", Quantity = 200, Unit = "grams", IsOptional = false },
                new RecipeIngredient { RecipeId = 2, Name = "Romaine Lettuce", Quantity = 2, Unit = "heads", IsOptional = false },
                new RecipeIngredient { RecipeId = 2, Name = "Caesar Dressing", Quantity = 0.25m, Unit = "cup", IsOptional = false }
            };
            context.RecipeIngredients.AddRange(ingredients);

            // Seed Menu Items
            var menuItems = new[]
            {
                new MenuItem { Name = "Margarita Pizza", Description = "Classic Italian pizza with fresh basil", Price = 18.99m, Category = MenuCategory.MainCourse, IsAvailable = true, RecipeId = 1 },
                new MenuItem { Name = "Caesar Salad", Description = "Fresh romaine with parmesan and croutons", Price = 12.99m, Category = MenuCategory.Appetizer, IsAvailable = true, RecipeId = 2 },
                new MenuItem { Name = "Chocolate Cake", Description = "Rich chocolate dessert", Price = 8.99m, Category = MenuCategory.Dessert, IsAvailable = true },
                new MenuItem { Name = "Italian Soda", Description = "Refreshing sparkling beverage", Price = 3.99m, Category = MenuCategory.Beverage, IsAvailable = true }
            };
            context.MenuItems.AddRange(menuItems);

            context.SaveChanges();
        }
    }
}