using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController(BreweryDbContext context) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> Create([FromBody] OrganizationDto dto)
	{
		var organization = new Organization
		{
			Id = Guid.NewGuid(),
			Name = dto.Name,
			Addresses = dto.Addresses
				.Where(a => !string.IsNullOrWhiteSpace(a))
				.Select(a => new StoreAddress
				{
					Id = Guid.NewGuid(),
					Address = a
				})
				.ToList()
		};

		context.Organizations.Add(organization);
		await context.SaveChangesAsync();

		var organizations = context.Organizations
			.Include(o => o.Addresses)
			.Select(o => new
			{
				o.Id,
				o.Name,
				Addresses = o.Addresses.Select(a => a.Address).ToList()
			})
			.ToList();

		return Ok(organizations);
	}
	
	  // Метод для получения всех организаций
    [HttpGet]
    public IActionResult GetAll()
    {
        var organizations = context.Organizations
            .Include(o => o.Addresses)
            .Select(o => new
            {
                o.Id,
                o.Name,
                Addresses = o.Addresses.Select(a => a.Address).ToList()
            })
            .ToList();

        return Ok(organizations);
    }
}