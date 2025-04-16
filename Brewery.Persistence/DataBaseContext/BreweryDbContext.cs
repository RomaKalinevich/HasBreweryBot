using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataBaseContext;

public class BreweryDbContext(DbContextOptions<BreweryDbContext> options) : DbContext(options)
{
	public DbSet<User> Users { get; set; }
	public DbSet<Order> Orders { get; set; } = null!;
	public DbSet<OrderItem> OrderItems { get; set; } = null!;
	public DbSet<Organization> Organizations { get; set; } = null!;
	public DbSet<ProductItem> ProductItems { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Конфигурация сущности User
		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(u => u.Id);

			entity.Property(u => u.TelegramId)
				.IsRequired();

			entity.HasIndex(u => u.TelegramId)
				.IsUnique();

			entity.Property(u => u.FullName)
				.HasMaxLength(100);

			entity.Property(u => u.Role)
				.HasConversion<string>()
				.IsRequired();

			entity.Property(u => u.IsActive)
				.IsRequired();

			entity.Property(u => u.CreatedAt)
				.IsRequired();
		});

		modelBuilder.Entity<Order>()
			.HasMany(o => o.Items)
			.WithOne(i => i.Order)
			.HasForeignKey(i => i.OrderId)
			.OnDelete(DeleteBehavior.Cascade);
		
	}
}