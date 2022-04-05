using AutoMapper;
using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
	private readonly IBasketRepository _repository;
	private readonly DiscountGrpcService _discountGrpcService;
	private readonly IMapper _mapper;
	private readonly IPublishEndpoint _publishEndpoint;


	public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService, IMapper mapper,
		IPublishEndpoint publishEndpoint)
	{
		_repository = repository;
		_discountGrpcService = discountGrpcService;
		_mapper = mapper;
		_publishEndpoint = publishEndpoint;
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

	[HttpPost("[action]")]
	public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
	{
		var basket = await _repository.GetBasket(basketCheckout.UserName);
		if (basket == null) return BadRequest();

		var basketCheckoutEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
		basketCheckoutEvent.TotalPrice = basket.TotalPrice;

		await _publishEndpoint.Publish(basketCheckoutEvent);
		await _repository.DeleteBasket(basket.UserName);

		return Accepted();
	}
}