using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumers;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
	private readonly ISender _sender;
	private readonly IMapper _mapper;
	private readonly ILogger<BasketCheckoutConsumer> _logger;

	public BasketCheckoutConsumer(ISender sender, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
	{
		_sender = sender;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
	{
		_logger.LogInformation("BasketCheckout Event was received {@Message}", context.Message);
		var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
		var result = await _sender.Send(command);
		_logger.LogInformation("BasketCheckout Event was received {@Result}", result);
	}
}