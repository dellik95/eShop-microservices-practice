using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.HttpServices;

public interface IOrderService
{
	Task<IEnumerable<OrderResponse>> GetOrdersByUserName(string userName);
}