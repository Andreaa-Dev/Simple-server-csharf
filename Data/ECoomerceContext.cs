using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Models;

public class ECommerceContext : DbContext
{
    public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}
