namespace Domain.Entities;

public class Order
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid OrganizationId { get; set; }
    public Guid StoreId { get; set; }
	public List<OrderItem> Items { get; set; } = new();
}