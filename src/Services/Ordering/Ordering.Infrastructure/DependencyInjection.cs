using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
	{
		services.AddDbContext<OrderContext>(o => { o.UseSqlServer("Connection string"); });
		services.AddScoped<IOrderRepository, OrderRepository>();

		return services;
	}
}