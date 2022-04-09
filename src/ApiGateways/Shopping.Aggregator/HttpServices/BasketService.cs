using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.HttpServices;

public class BasketService : IBasketService
{
	private readonly HttpClient _client;

	public BasketService(HttpClient client)
	{
		_client = client;
	}

	public async Task<Basket> GetBasket(string userName)
	{
		var response = await _client.GetAsync($"api/v1/Basket/{userName}");
		var basket = await response.ReadContentAs<Basket>();
		return basket;
	}
}