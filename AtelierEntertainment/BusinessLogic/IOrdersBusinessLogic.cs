using System.Collections.Generic;
using System.ComponentModel;
using AtelierEntertainment.BusinessLogic.Dtos;

namespace AtelierEntertainment.BusinessLogic
{
	public interface IOrdersBusinessLogic
	{
		IEnumerable<OrderDto> GetOrdersByCustomerId(int customerId);
		OrderDto GetOrderById(int id);
		OrderDto CreateOrder(CreateOrderDto dto);
	}
}