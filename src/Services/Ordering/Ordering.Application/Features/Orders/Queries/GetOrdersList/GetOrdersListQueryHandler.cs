using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

internal class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrderVm>>
{
	private readonly IOrderRepository _repository;
	private readonly IMapper _mapper;


	public GetOrdersListQueryHandler(IOrderRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<List<OrderVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
	{
		var orders = await _repository.GetOrdersByUserName(request.UserName);
		var mapped = _mapper.Map<IEnumerable<OrderVm>>(orders);
		return mapped.ToList();
	}
}