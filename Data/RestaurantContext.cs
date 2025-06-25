using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Order configuration
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(10,2)");
                entity.HasOne(e => e.User)
                      .WithMany(e => e.Orders)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // OrderItem configuration
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10,2)");
                entity.HasOne(e => e.Order)
                      .WithMany(e => e.OrderItems)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.MenuItem)
                      .WithMany(e => e.OrderItems)
                      .HasForeignKey(e => e.MenuItemId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // MenuItem configuration
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
                entity.HasOne(e => e.Recipe)
                      .WithMany(e => e.MenuItems)
                      .HasForeignKey(e => e.RecipeId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Recipe configuration
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            // RecipeIngredient configuration
            modelBuilder.Entity<RecipeIngredient>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Quantity).HasColumnType("decimal(10,2)");
                entity.HasOne(e => e.Recipe)
                      .WithMany(e => e.Ingredients)
                      .HasForeignKey(e => e.RecipeId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}