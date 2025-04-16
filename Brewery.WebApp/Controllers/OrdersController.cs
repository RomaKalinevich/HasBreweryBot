using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataBaseContext;

namespace Brewery.WebApp.Controllers;

[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly BreweryDbContext _db;

    public OrdersController(BreweryDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderDto dto)
    {
        var order = new Order
        {
            OrganizationName = dto.Organization,
            Items = dto.Items,
            CreatedAt = DateTime.UtcNow
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return Ok();
    }

    public class OrderDto
    {
        public string Organization { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}