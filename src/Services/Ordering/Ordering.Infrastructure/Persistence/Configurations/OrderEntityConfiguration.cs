using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence.Configurations;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasData(new List<Order>()
		{
			new Order()
			{
				UserName = "swn", FirstName = "Test", LastName = "Test", EmailAddress = "Test@gmail.com",
				AddressLine = "Test", Country = "Test", TotalPrice = 350
			},
			new Order()
			{
				UserName = "swn1", FirstName = "Test1", LastName = "Test1", EmailAddress = "Test1@gmail.com",
				AddressLine = "Test", Country = "Test", TotalPrice = 350
			}
		});
	}
}