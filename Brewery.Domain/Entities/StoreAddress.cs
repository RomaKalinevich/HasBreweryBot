namespace Domain.Entities;

public class StoreAddress
{
	public Guid Id { get; set; }
    public string Address { get; set; } = null!;
    public Guid OrganizationId { get; set; }

    public Organization Organization { get; set; } = null!;
}