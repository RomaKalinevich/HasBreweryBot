using Domain.Entities;

namespace Brewery.WebApp.Models;

public class Application
{
	public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public int OrganizationId { get; set; }
    public Organization Organization { get; set; }

    public List<ProductItem> Items { get; set; } = new();
}