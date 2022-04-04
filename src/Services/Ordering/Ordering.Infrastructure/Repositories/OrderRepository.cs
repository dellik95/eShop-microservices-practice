using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
	public OrderRepository(DbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) =>
		await FindAsync(e => e.UserName == userName);
}