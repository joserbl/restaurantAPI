using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrderAsync(string username, CreateOrderRequest request);
        Task<List<OrderResponse>> GetUserOrdersAsync(string username);
        Task<OrderResponse?> GetOrderByIdAsync(int orderId, string username);
        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus status);
    }
}