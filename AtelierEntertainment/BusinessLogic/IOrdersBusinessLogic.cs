using System.Collections.Generic;
using AtelierEntertainment.BusinessLogic.Dtos;
using AutoMapper;

namespace AtelierEntertainment.BusinessLogic
{
	public interface IOrdersBusinessLogic
	{
		IEnumerable<OrderDto> GetOrdersByCustomerId(int customerId);
		OrderDto GetOrderById(int Id);
		OrderDto CreateOrder(CreateOrderDto dto);
	}

	public class OrdersBusinessLogic : IOrdersBusinessLogic
	{
		private readonly IMapper _mapper;

		public OrdersBusinessLogic(IMapper mapper)
		{
			_mapper = mapper;
		}

		public IEnumerable<OrderDto> GetOrdersByCustomerId(int customerId)
		{
			throw new System.NotImplementedException();
		}

		public OrderDto GetOrderById(int Id)
		{
			throw new System.NotImplementedException();
		}

		public OrderDto CreateOrder(CreateOrderDto dto)
		{
			throw new System.NotImplementedException();
		}
	}
}