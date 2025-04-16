namespace Domain.Entities;

public class ProductItem
{
	public int Id { get; set; }
    public Guid ApplicationId { get; set; }
    public string Name { get; set; }
    public string Volume { get; set; }
    public int KegCount { get; set; }
}