using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using RestaurantAPI.DTOs;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

       [HttpPost]
        public async Task<ActionResult<OrderResponse>> CreateOrder(CreateOrderRequest request)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) return Unauthorized();

            var order = await _orderService.CreateOrderAsync(username, request);
            if (order == null) return BadRequest("Unable to create order");

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetUserOrders()
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) return Unauthorized();

            var orders = await _orderService.GetUserOrdersAsync(username);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrder(int id)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null) return Unauthorized();

            var order = await _orderService.GetOrderByIdAsync(id, username);
            if (order == null) return NotFound();

            return Ok(order);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult> UpdateOrderStatus(int id, [FromBody] OrderStatus status)
        {
            var success = await _orderService.UpdateOrderStatusAsync(id, status);
            if (!success) return NotFound();

            return NoContent();
        }
 
    }
}