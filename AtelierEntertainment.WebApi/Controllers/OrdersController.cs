using System.Collections.Generic;
using AtelierEntertainment.BusinessLogic;
using AtelierEntertainment.BusinessLogic.Dtos;
using AtelierEntertainment.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AtelierEntertainment.WebApi.Controllers
{
	[Produces("application/json")]
	[Route("api")]
	public class OrdersController : CustomApiControllerBase
	{
		private readonly IOrdersBusinessLogic _ordersBusinessLogic;

		public OrdersController(IOrdersBusinessLogic ordersBusinessLogic)
		{
			_ordersBusinessLogic = ordersBusinessLogic;
		}

		[HttpGet("customers/{customerId}/orders")]
		public IEnumerable<OrderDto> GetCustomerOrders(int customerId)
		{
			return _ordersBusinessLogic.GetOrdersByCustomerId(customerId);
		}

		[HttpGet("orders/{id}")]
		public OrderDto GetOrder(int id)
		{
			return _ordersBusinessLogic.GetOrderById(id);
		}

		[Consumes("application/json")]
		[HttpPost("orders")]
		[ProducesResponseType(typeof(OrderDto), 201)]
		public IActionResult CreateOrder([FromBody] CreateOrderDto dto)
		{
			var createdDto = _ordersBusinessLogic.CreateOrder(dto);
			return this.Created(createdDto);
		}
	}
}