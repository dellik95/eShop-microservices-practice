using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
{
	private readonly IOrderRepository _repository;
	private readonly ILogger<DeleteOrderCommandHandler> _logger;

	public DeleteOrderCommandHandler(IOrderRepository repository, ILogger<DeleteOrderCommandHandler> logger)
	{
		_repository = repository;
		_logger = logger;
	}

	public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.FindById(request.Id);
		if (entity == null)
		{
			_logger.LogWarning($"Order with Id = {request.Id} not exist in Database");
			throw new NotFoundException(nameof(Order), request.Id);
		}

		await _repository.DeleteAsync(entity);
		_logger.LogWarning($"Order with Id = {request.Id} successfully deleted in Database");
		return Unit.Value;
	}
}