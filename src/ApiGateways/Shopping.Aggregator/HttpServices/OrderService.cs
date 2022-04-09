using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.HttpServices;

public class OrderService : IOrderService
{
	private readonly HttpClient _client;

	public OrderService(HttpClient client)
	{
		_client = client;
	}

	public async Task<IEnumerable<OrderResponse>> GetOrdersByUserName(string userName)
	{
		var response = await _client.GetAsync($"api/v1/Order/{userName}");
		var orderData = await response.ReadContentAs<List<OrderResponse>>();
		return orderData;
	}
}