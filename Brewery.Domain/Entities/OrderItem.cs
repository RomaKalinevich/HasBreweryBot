namespace Domain.Entities;

public class OrderItem
{
	public Guid Id { get; set; } = Guid.NewGuid();

	public string BeerName { get; set; } = string.Empty;

	public int VolumeLiters { get; set; }

	public int KegCount { get; set; }
	public Guid OrderId { get; set; }
	public Order Order { get; set; } = null!;
}