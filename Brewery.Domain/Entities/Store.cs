namespace Domain.Entities;

public class Store
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Address { get; set; } = string.Empty;

	public Guid OrganizationId { get; set; }
	public Organization Organization { get; set; } = null!;
}