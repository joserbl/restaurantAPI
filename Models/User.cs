using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Navigation properties
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public enum UserRole
    {
        Customer,
        Staff,
        Admin
    }
}