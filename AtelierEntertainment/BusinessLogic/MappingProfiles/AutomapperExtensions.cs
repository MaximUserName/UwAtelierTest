using System;
using System.Collections.Generic;
using System.Text;
using AtelierEntertainment.BusinessLogic.Dtos;
using AutoMapper;

namespace AtelierEntertainment.BusinessLogic.MappingProfiles
{
	
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			this.CreateMap<Order, OrderDto>()
				;

			this.CreateMap<orderItem, OrderItemDto>()
				;

			this.CreateMap<CreateOrderDto, Order>()
				//.ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
				//.ForAllOtherMembers(opt => opt.Ignore())
				;

			this.CreateMap<OrderItemDto, orderItem>()
				;
			
		}
	}
}
