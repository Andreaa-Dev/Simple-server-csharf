using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models;
using System.Linq;
using ECommerceAPI.Data;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        return Ok(InMemoryData.Products);
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        var product = InMemoryData.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> PostProduct(Product product)
    {
        if (InMemoryData.Products.Any(p => p.Id == product.Id))
        {
            return Conflict("Product with the same ID already exists.");
        }

        InMemoryData.Products.Add(product);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult PutProduct(int id, Product product)
    {
        var index = InMemoryData.Products.FindIndex(p => p.Id == id);

        if (index == -1)
        {
            return NotFound();
        }

        InMemoryData.Products[index] = product;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var product = InMemoryData.Products.FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        InMemoryData.Products.Remove(product);

        return NoContent();
    }
}
