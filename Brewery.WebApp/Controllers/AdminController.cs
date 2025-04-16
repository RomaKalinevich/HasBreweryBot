using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.DataBaseContext;

namespace Brewery.WebApp.Controllers;

public class AdminController(BreweryDbContext context) : Controller
{
	private readonly BreweryDbContext _context = context;

	[HttpGet]
	public IActionResult Organizations()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> AddOrganization(string name, string address)
	{
		if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address))
		{
			ModelState.AddModelError("", "Все поля обязательны");
			return View("Organizations");
		}

		var org = new Organization
		{
			Id = Guid.NewGuid(),
			Name = name,
			Address = address,
			CreatedAt = DateTime.UtcNow
		};

		_context.Organizations.Add(org);
		await _context.SaveChangesAsync();

		ViewBag.Success = true;
		return View("Organizations");
	}
}