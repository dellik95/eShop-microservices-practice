using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;
	private readonly IEmailService _emailService;
	private readonly ILogger<CheckoutOrderCommandHandler> _logger;

	public CheckoutOrderCommandHandler(IOrderRepository repository, IMapper mapper, IEmailService emailService,
		ILogger<CheckoutOrderCommandHandler> logger)
	{
		_repository = repository;
		_mapper = mapper;
		_emailService = emailService;
		_logger = logger;
	}

	public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var entity = _mapper.Map<Order>(request);
			var created = await _repository.AddAsync(entity);
			_logger.LogInformation($"");
			await SendEmail(created);
			return created.Id;
		}
		catch (Exception e)
		{
			_logger.LogError(e.Message, e);
			throw;
		}
	}

	private async Task SendEmail(Order order)
	{
		try
		{
			var email = new Email()
			{
				Body = $"Order with Id = {order.Id} was created",
				Subject = "New order in system",
				To = "test@test.com"
			};

			await _emailService.Send(email);
		}
		catch (Exception e)
		{
			_logger.LogError(e.Message);
			throw;
		}
	}
}