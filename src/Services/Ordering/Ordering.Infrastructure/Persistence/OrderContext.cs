using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence.Configurations;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext : DbContext
{
	public OrderContext(DbContextOptions<OrderContext> options) : base(options)
	{
		MigrateWhenNeed();
	}

	private void MigrateWhenNeed()
	{
		var pendingMigrations = Database.GetPendingMigrations();
		if (pendingMigrations.Any())
		{
			Database.Migrate();
		}
	}

	public DbSet<Order> Orders { get; set; }

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
		CancellationToken cancellationToken = new CancellationToken())
	{
		var entries = ChangeTracker.Entries<EntityBase>();
		foreach (var entry in entries)
		{
			switch (entry.State)
			{
				case EntityState.Modified:
					entry.Entity.ModifiedOn = DateTime.Now;
					entry.Entity.ModifiedBy = "test";
					break;
				case EntityState.Added:
					entry.Entity.CreatedOn = DateTime.Now;
					entry.Entity.CreatedBy = "test";
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
	}
}