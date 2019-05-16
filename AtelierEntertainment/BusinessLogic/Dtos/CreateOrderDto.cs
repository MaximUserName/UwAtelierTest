using System.Collections.Generic;
using System.Linq;

namespace AtelierEntertainment.BusinessLogic.Dtos
{
	public class CreateOrderDto
	{
		public CreateOrderDto()
		{
			Items = Enumerable.Empty<OrderItemDto>().ToList();
		}
		public int CustomerId { get; set; }
		public List<OrderItemDto> Items { get; set; }
		public decimal Total { get; internal set; }
	}
}