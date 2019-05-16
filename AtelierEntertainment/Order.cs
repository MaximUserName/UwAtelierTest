using System.Collections.Generic;
using System.Linq;

namespace AtelierEntertainment
{
    public class Order
    {
	    public Order()
	    {
		    this.Items = Enumerable.Empty<orderItem>().ToList();
	    }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        public List<orderItem> Items { get; set; }
        public decimal Total { get; internal set; }
    }

    public class orderItem
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
    }
}