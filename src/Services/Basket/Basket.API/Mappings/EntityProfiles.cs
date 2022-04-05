using AutoMapper;
using EventBus.Messages.Events;

namespace Basket.API.Entities.Mappings;

public class EntityProfiles : Profile
{
	public EntityProfiles()
	{
		CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
	}
}