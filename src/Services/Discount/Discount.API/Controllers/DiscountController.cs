using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
	private readonly IDiscountRepository _repository;

	public DiscountController(IDiscountRepository repository)
	{
		_repository = repository;
	}


	[HttpGet("{productName}", Name = nameof(GetDiscount))]
	public async Task<IActionResult> GetDiscount(string productName)
	{
		var coupon = await _repository.GetDiscount(productName);
		return Ok(coupon);
	}


	[HttpPost(Name = nameof(CreateDiscount))]
	public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
	{
		var created = await _repository.CreateDiscount(coupon);
		return CreatedAtRoute(nameof(GetDiscount), new
		{
			productName = coupon.ProductName
		}, coupon);
	}


	[HttpPut(Name = nameof(UpdateDiscount))]
	public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
	{
		var updated = await _repository.UpdateDiscount(coupon);
		return Ok(updated);
	}


	[HttpDelete("{productName}", Name = nameof(DeleteDiscount))]
	public async Task<IActionResult> DeleteDiscount(string productName)
	{
		var affected = await _repository.DeleteDiscount(productName);

		return (affected) ? NoContent() : BadRequest();
	}
}