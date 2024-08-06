using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models;
using ECommerceAPI.Data;

using System.Collections.Generic;
using System.Linq;


[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    // GET: api/orders
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetOrders()
    {
        // Include order items and products in the response
        var orders = InMemoryData.Orders
            .Select(o => new Order
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItem
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Product = InMemoryData.Products.FirstOrDefault(p => p.Id == oi.ProductId)
                }).ToList()
            }).ToList();

        return Ok(orders);
    }

    // GET: api/orders/5
    [HttpGet("{id}")]
    public ActionResult<Order> GetOrder(int id)
    {
        var order = InMemoryData.Orders
            .Where(o => o.Id == id)
            .Select(o => new Order
            {
                Id = o.Id,
                UserId = o.UserId,
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems.Select(oi => new OrderItem
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity,
                    Product = InMemoryData.Products.FirstOrDefault(p => p.Id == oi.ProductId)
                }).ToList()
            }).SingleOrDefault();

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // POST: api/orders
    [HttpPost]
    public ActionResult<Order> PostOrder(Order order)
    {
        // Ensure that the Order ID is unique
        if (InMemoryData.Orders.Any(o => o.Id == order.Id))
        {
            return Conflict("Order with the same ID already exists.");
        }

        // Add the new order to the list
        InMemoryData.Orders.Add(order);

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }
}
