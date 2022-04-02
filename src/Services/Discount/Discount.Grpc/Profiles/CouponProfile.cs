﻿using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Profiles;

public class CouponProfile : Profile
{
	public CouponProfile()
	{
		CreateMap<Coupon, CouponModel>().ReverseMap();
	}
}