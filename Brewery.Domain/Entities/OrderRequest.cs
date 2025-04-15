namespace Domain.Entities;

public class OrderRequest
{
	public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = null!;
    public Guid CreatedById { get; set; }
    public Guid? AssignedDriverId { get; set; }

    public User? CreatedBy { get; set; }
    public User? AssignedDriver { get; set; }
}