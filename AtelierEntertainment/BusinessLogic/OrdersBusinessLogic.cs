using System.Collections.Generic;
using AtelierEntertainment.BusinessLogic.Dtos;
using AutoMapper;

namespace AtelierEntertainment.BusinessLogic
{
	public class OrdersBusinessLogic : IOrdersBusinessLogic
	{
		private readonly IMapper _mapper;
		private readonly OrderService _orderService;
		private OrderDataContext _context;

		public OrdersBusinessLogic(IMapper mapper, OrderService orderService)
		{
			_mapper = mapper;
			_orderService = orderService;
			_context = new OrderDataContext();
			
		}

		public IEnumerable<OrderDto> GetOrdersByCustomerId(int customerId)
		{
			var orders = _orderService.GetOrdersByCustomerId(customerId);
			return _mapper.Map<IEnumerable<OrderDto>>(orders);
		}

		public OrderDto GetOrderById(int id)
		{
			var order = OrderDataContext.LoadOrder(id);
			return _mapper.Map<OrderDto>(order);
		}

		public OrderDto CreateOrder(CreateOrderDto dto)
		{
			var order = _mapper.Map<Order>(dto);
			order.Customer = _context.GetCustomer(dto.CustomerId);
			
			_orderService.CreateOrder(order);
			var orderfromDb = OrderDataContext.LoadOrder(order.Id);
			return _mapper.Map<OrderDto>(orderfromDb);
		}
	}
}