namespace Shopping.Aggregator.HttpServices;

public static class DependencyInjection
{
	public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<IOrderService, OrderService>(config =>
		{
			config.BaseAddress = new Uri(configuration["ApiSettings:OrderingUrl"]);
		});
		services.AddHttpClient<ICatalogService, CatalogService>(config =>
		{
			config.BaseAddress = new Uri(configuration["ApiSettings:CatalogUrl"]);
		});
		services.AddHttpClient<IBasketService, BasketService>(config =>
		{
			config.BaseAddress = new Uri(configuration["ApiSettings:BasketUrl"]);
		});
		return services;
	}
}