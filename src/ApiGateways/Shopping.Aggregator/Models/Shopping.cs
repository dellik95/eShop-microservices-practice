namespace Shopping.Aggregator.Models;

public class ShoppingModel
{
	public string UserName { get; set; }
	public Basket BasketWithProducts { get; set; }
	public IEnumerable<OrderResponse> Orders { get; set; }
}