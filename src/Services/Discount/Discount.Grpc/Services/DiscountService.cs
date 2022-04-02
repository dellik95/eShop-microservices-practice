using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Newtonsoft.Json;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
	private readonly IDiscountRepository _repository;
	private readonly ILogger<DiscountService> _logger;
	private readonly IMapper _mapper;

	public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
	{
		_repository = repository;
		_logger = logger;
		_mapper = mapper;
	}


	public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
	{
		var discount = await _repository.GetDiscount(request.ProductName);
		if (discount == null)
			throw new RpcException(new Status(StatusCode.NotFound,
				$"Discount with productName = {request.ProductName} not found"));

		_logger.Log(LogLevel.Information,
			$"Execute GetDiscount with request = {JsonConvert.SerializeObject(discount)}");
		return _mapper.Map<CouponModel>(discount);
	}

	public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
	{
		var discount = _mapper.Map<Coupon>(request.Coupon);
		var affected = await _repository.CreateDiscount(discount);
		if (!affected)
		{
			throw new RpcException(new Status(StatusCode.Unknown, "Error on CreateDiscount"));
		}

		_logger.Log(LogLevel.Information,
			$"Execute CreateDiscount with request = {JsonConvert.SerializeObject(discount)}");
		return request.Coupon;
	}

	public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
		ServerCallContext context)
	{
		var affected = await _repository.DeleteDiscount(request.ProductName);
		if (!affected)
		{
			throw new RpcException(new Status(StatusCode.Unknown, "Error on DeleteDiscount"));
		}

		_logger.Log(LogLevel.Information,
			$"Execute DeleteDiscount with productName = {request.ProductName}");
		return new DeleteDiscountResponse()
		{
			Success = affected
		};
	}


	public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
	{
		var discount = _mapper.Map<Coupon>(request.Coupon);
		var affected = await _repository.UpdateDiscount(discount);
		if (!affected)
		{
			throw new RpcException(new Status(StatusCode.Unknown, "Error on UpdateDiscount"));
		}

		_logger.Log(LogLevel.Information,
			$"Execute UpdateDiscount with request = {JsonConvert.SerializeObject(discount)}");
		return request.Coupon;
	}
}