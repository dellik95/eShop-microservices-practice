using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.HttpServices;

public interface IBasketService
{
	Task<Basket> GetBasket(string userName);
}