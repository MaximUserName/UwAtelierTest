using System.Collections.Generic;
using AtelierEntertainment.BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace AtelierEntertainment.WebApi.Controllers
{
	[Route("api")]
	public class OrdersController : CustomApiControllerBase
	{
		private readonly IOrdersBusinessLogic _ordersBusinessLogic;

		public OrdersController(IOrdersBusinessLogic ordersBusinessLogic)
		{
			_ordersBusinessLogic = ordersBusinessLogic;
		}
		// GET api/values
		[HttpGet("customers/{customerId}/orders")]
		public ActionResult<IEnumerable<string>> GetCustomerOrders(int customerId)
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/values/5
		[HttpGet("orders/{id}")]
		public ActionResult<string> GetOrder(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost("orders")]
		public void CreateOrder([FromBody] string value)
		{
		}
	}
}