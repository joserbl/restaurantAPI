using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.DTOs
{
   public class CreateOrderRequest
    {
        public List<OrderItemRequest> Items { get; set; } = new();
        public string? SpecialInstructions { get; set; }
    }

    public class OrderItemRequest
    {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }

    public class OrderResponse
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string? SpecialInstructions { get; set; }
        public List<OrderItemResponse> Items { get; set; } = new();
    }

    public class OrderItemResponse
    {
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Notes { get; set; }
    }
}