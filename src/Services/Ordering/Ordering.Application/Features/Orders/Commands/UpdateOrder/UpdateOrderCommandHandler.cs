using System.Globalization;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Exceptions;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;
	private readonly ILogger<UpdateOrderCommandHandler> _logger;

	public UpdateOrderCommandHandler(IOrderRepository repository, IMapper mapper,
		ILogger<UpdateOrderCommandHandler> logger)
	{
		_repository = repository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.FindById(request.Id);
		if (entity == null)
		{
			_logger.LogWarning($"Order with Id = {request.Id} not exist in Database");
			throw new NotFoundException(nameof(Order), request.Id);
		}

		_mapper.Map(request, entity, typeof(UpdateOrderCommand), typeof(Order));
		await _repository.UpdateAsync(entity);
		_logger.LogWarning($"Order with Id = {request.Id} successfully updated in Database");
		return Unit.Value;
	}
}