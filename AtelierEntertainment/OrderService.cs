using System;
using System.Collections.Generic;
using System.Linq;

namespace AtelierEntertainment
{
    public class OrderService
    {
	    public void CreateOrder(Order order)
        {
            if (order.Customer.Country == "AU")
                order.Total = Convert.ToDecimal(order.Items.Sum(_ => _.Price) * 1.1);
            else if (order.Customer.Country == "UK")
                order.Total = Convert.ToDecimal(order.Items.Sum(_ => _.Price) * 1.2);

            var dataContext = new OrderDataContext();

            dataContext.CreateOrder(order);
        }

        
        public IEnumerable<Order> GetOrdersByCustomerId(int id)
        {
	        var dataContext = new OrderDataContext();
			return dataContext.GetCustomerOrders(id);
        }
    }
}
