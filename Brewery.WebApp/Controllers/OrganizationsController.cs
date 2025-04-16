using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;

namespace Brewery.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController(BreweryDbContext context) : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<IEnumerable<object>>> Get()
	{
		var orgs = await context.Organizations
			.Select(o => new { o.Id, o.Name })
			.ToListAsync();

		return Ok(orgs);
	}
}