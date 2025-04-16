namespace Domain.Entities;

public class Organization
{
	public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}