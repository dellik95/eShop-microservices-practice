using System.Text;
using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
	private readonly IDistributedCache _cache;

	public BasketRepository(IDistributedCache cache)
	{
		_cache = cache;
	}

	public async Task<ShoppingCart?> GetBasket(string userName)
	{
		var data = await _cache.GetStringAsync(userName);
		if (string.IsNullOrEmpty(data)) return null;
		return JsonConvert.DeserializeObject<ShoppingCart>(data);
	}

	public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
	{
		var data = JsonConvert.SerializeObject(cart);
		await _cache.SetStringAsync(cart.UserName, data);
		return await GetBasket(cart.UserName);
	}

	public async Task DeleteBasket(string userName)
	{
		await _cache.RemoveAsync(userName);
	}
}