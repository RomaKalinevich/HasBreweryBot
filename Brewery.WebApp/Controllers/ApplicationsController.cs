using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Persistence.DataBaseContext;

namespace Brewery.WebApp.Controllers;

public class ApplicationsController(BreweryDbContext context) : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Organizations = context.Organizations
            .Select(o => new SelectListItem
            {
                Value = o.Id.ToString(),
                Text = o.Name
            }).ToList();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string asd)
    {
        var test = context.Organizations
            .ToList();
        
        await context.SaveChangesAsync();

        return RedirectToAction("Create");
    }
}