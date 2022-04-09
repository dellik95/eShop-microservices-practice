using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.HttpServices;
using Shopping.Aggregator.Models;


namespace Shopping.Aggregator.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ShoppingController : ControllerBase
{
	private readonly IOrderService _orderService;
	private readonly ICatalogService _catalogService;
	private readonly IBasketService _basketService;

	public ShoppingController(IOrderService orderService, ICatalogService catalogService, IBasketService basketService)
	{
		_orderService = orderService;
		_catalogService = catalogService;
		_basketService = basketService;
	}


	[HttpGet("{userName}")]
	public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
	{
		var basket = await _basketService.GetBasket(userName);

		foreach (var basketItem in basket.Items)
		{
			var product = await _catalogService.GetCatalog(basketItem.ProductId);

			basketItem.ProductName = product.Name;
			basketItem.Category = product.Category;
			basketItem.Summary = product.Summary;
			basketItem.Description = product.Description;
			basketItem.ImageFile = product.ImageFile;
		}

		var orders = await _orderService.GetOrdersByUserName(userName);

		var shopping = new ShoppingModel()
		{
			UserName = userName,
			Orders = orders,
			BasketWithProducts = basket
		};
		return Ok(shopping);
	}
}