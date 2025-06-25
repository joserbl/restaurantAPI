using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly RestaurantContext _context;
        public OrderService(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<OrderResponse> CreateOrderAsync(string username, CreateOrderRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            var menuItemIds = request.Items.Select(i => i.MenuItemId).ToList();
            var menuItems = await _context.MenuItems
                .Where(m => menuItemIds.Contains(m.Id) && m.IsAvailable)
                .ToListAsync();

            if (menuItems.Count != request.Items.Count) return null;

            var order = new Order
            {
                UserId = user.Id,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                SpecialInstructions = request.SpecialInstructions
            };

            foreach (var itemRequest in request.Items)
            {
                var menuItem = menuItems.First(m => m.Id == itemRequest.MenuItemId);
                var orderItem = new OrderItem
                {
                    MenuItemId = menuItem.Id,
                    Quantity = itemRequest.Quantity,
                    UnitPrice = menuItem.Price,
                    Notes = itemRequest.Notes
                };
                order.OrderItems.Add(orderItem);
            }

            order.TotalAmount = order.OrderItems.Sum(oi => oi.TotalPrice);

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(order.Id, username);
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(int orderId, string username)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.User.Username == username);

            return order != null ? MapToOrderResponse(order) : null;

        }

        public async Task<List<OrderResponse>> GetUserOrdersAsync(string username)
        {
                var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.User.Username == username)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return orders.Select(MapToOrderResponse).ToList();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            order.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }
        
        private OrderResponse MapToOrderResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                SpecialInstructions = order.SpecialInstructions,
                Items = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    MenuItemName = oi.MenuItem.Name,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    TotalPrice = oi.TotalPrice,
                    Notes = oi.Notes
                }).ToList()
            };
        }
    }
}