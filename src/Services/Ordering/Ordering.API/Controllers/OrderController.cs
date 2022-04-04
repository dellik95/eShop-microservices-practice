using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
	private readonly ISender _sender;


	public OrderController(ISender sender)
	{
		_sender = sender;
	}


	[HttpGet("{userName}", Name = "GetOrder")]
	public async Task<IActionResult> GetOrderByUserName(string userName)
	{
		var order = await _sender.Send(new GetOrdersListQuery(userName));
		return Ok(order);
	}


	[HttpPost("checkout",Name = "Checkout")]
	public async Task<IActionResult> CheckoutOrder([FromBody] CheckoutOrderCommand command)
	{
		var result = await _sender.Send(command);
		return Ok(result);
	}

	[HttpPost("update",Name = "UpdateOrder")]
	public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
	{
		await _sender.Send(command);
		return NoContent();
	}


	[HttpPost("{id:int}", Name = "DeleteOrder")]
	public async Task<IActionResult> DeleteOrder(int id)
	{
		await _sender.Send(new DeleteOrderCommand()
		{
			Id = id
		});
		return NoContent();
	}
}