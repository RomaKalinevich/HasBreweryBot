using Domain.Enums;

namespace Domain.Entities;
public class User
{
	public Guid Id { get; set; }
    public string TelegramId { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public Role Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}