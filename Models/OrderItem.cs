using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
   public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Notes { get; set; }
        
        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual MenuItem MenuItem { get; set; } = null!;
        
        // Calculated property
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}