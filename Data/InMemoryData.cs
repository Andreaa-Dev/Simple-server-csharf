using ECommerceAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceAPI.Data
{
    public static class InMemoryData
    {
        static InMemoryData()
        {
            // Initialize Products
            Products.Add(new Product { Id = 1, Name = "Laptop", Description = "A high-performance laptop.", Price = 999.99m, Stock = 10 });
            Products.Add(new Product { Id = 2, Name = "Smartphone", Description = "A latest model smartphone.", Price = 499.99m, Stock = 25 });
            Products.Add(new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones.", Price = 199.99m, Stock = 50 });

            // Initialize Users
            Users.Add(new User { Id = 1, Username = "admin", Password = "password123", Email = "admin@example.com" });

            // Initialize Orders
            Orders.Add(new Order
            {
                Id = 1,
                UserId = 1,
                OrderDate = DateTime.UtcNow.AddDays(-1),
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 1, ProductId = 1, Quantity = 1 },
                    new OrderItem { Id = 2, ProductId = 2, Quantity = 2 }
                }
            });

            Orders.Add(new Order
            {
                Id = 2,
                UserId = 1,
                OrderDate = DateTime.UtcNow,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 3, ProductId = 3, Quantity = 3 }
                }
            });
        }

        public static List<Product> Products { get; } = new List<Product>();
        public static List<User> Users { get; } = new List<User>();
        public static List<Order> Orders { get; } = new List<Order>();
    }
}
