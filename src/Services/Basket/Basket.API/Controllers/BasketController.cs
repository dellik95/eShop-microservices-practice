using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
	private readonly IBasketRepository _repository;
	private readonly DiscountGrpcService _discountGrpcService;


	public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService)
	{
		_repository = repository;
		_discountGrpcService = discountGrpcService;
	}

	[HttpGet("{userName}")]
	public async Task<IActionResult> GetBasket(string userName)
	{
		var basket = await _repository.GetBasket(userName);
		return Ok(basket ?? new ShoppingCart(userName));
	}

	[HttpPost]
	public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart cart)
	{
		foreach (var item in cart.Items)
		{
			var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
			item.Price -= coupon.Amount;
		}

		var basket = await _repository.UpdateBasket(cart);
		return Ok(basket);
	}


	[HttpDelete("{userName}")]
	public async Task<IActionResult> ClearBasket(string userName)
	{
		await _repository.DeleteBasket(userName);
		return NoContent();
	}

	[HttpPost("{userName}")]
	public IActionResult CheckOut(string userName)
	{
		return Ok();
	}
}