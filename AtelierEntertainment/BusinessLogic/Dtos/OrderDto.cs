using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierEntertainment.BusinessLogic.Dtos
{
	public class OrderDto
	{
		public OrderDto()
		{
			Items = Enumerable.Empty<OrderItemDto>().ToList();
		}

		public int Id { get; set; }
		public int CustomerId { get; set; }
		public List<OrderItemDto> Items { get; set; }
		public decimal Total { get; internal set; }
	}
}
