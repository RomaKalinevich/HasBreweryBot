namespace Domain.Entities;

public class Order
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public string OrganizationName { get; set; } = string.Empty;
	public List<OrderItem> Items { get; set; } = new();
}