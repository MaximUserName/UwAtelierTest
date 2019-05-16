using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using AtelierEntertainment.Exceptions;

namespace AtelierEntertainment
{
	public class OrderDataContext
	{
		private const string ConnectionString =
			//"Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";
			//"Server=.;Database==AtelierDb;Integrated Security=true;";
			"Server=.;Database=AtelierDb;Trusted_Connection=True;";

		public void CreateOrder(Order order)
		{
			var conn = new SqlConnection(ConnectionString);
			conn.Open();
			var cmd = conn.CreateCommand();

			cmd.CommandText = $"INSERT INTO dbo.Orders (CustomerId, Total) VALUES ({order.Customer.Id}, {order.Total})" +
			                  "SELECT SCOPE_IDENTITY()";

			//cmd.ExecuteNonQuery();

			order.Id = (int)(decimal)cmd.ExecuteScalar();

			foreach (var item in order.Items)
			{
				cmd = conn.CreateCommand();

				cmd.CommandText =
					$"INSERT INTO dbo.OrderItems (OrderId, Code, Description, Price) VALUES ({order.Id}, '{item.Code}', '{item.Description}', {item.Price});";

				cmd.ExecuteNonQuery();
			}
			conn.Close();
		}



		public IEnumerable<Order> GetCustomerOrders(int customerId)
		{
			using (var conn = new SqlConnection(ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					//cmd.CommandText = $"SELECT * FROM dbo.Orders WHERE CustomerId = {customerId}";
					cmd.CommandText =
						$@"SELECT o.Id as OrderId, o.Total, o.CustomerId, oi.Code, oi.Description, oi.Price
FROM dbo.Orders as o join OrderItems as oi on o.Id = oi.OrderId WHERE o.CustomerId = {customerId}";
					var reader = cmd.ExecuteReader();
					var flatList = new List<OrderOrderItem>();

					while (reader.Read())
					{
						flatList.Add(new OrderOrderItem()
						{
							OrderId = Convert.ToInt32(reader["OrderId"]),
							Total = Convert.ToDecimal(reader["Total"]),

							Code = Convert.ToString(reader["Code"]),
							Description = Convert.ToString(reader["Description"]),
							Price = Convert.ToSingle(reader["Price"]),
						});
					}

					return from item in flatList
						   group item by item.OrderId
						into g
						   orderby g.Key
						   select new Order()
						   {
							   Id = g.Key,
							   CustomerId = customerId,
							   Total = g.First().Total,
							   Items = g.Select(e => new orderItem()
							   {
								   Code = e.Code,
								   Description = e.Description,
								   Price = e.Price,
							   }).ToList()
						   };

				}


			}
		}

		public static Order LoadOrder(int id)
		{
			var conn = new SqlConnection(ConnectionString);
			conn.Open();
			var cmd = conn.CreateCommand();

			cmd.CommandText = $"SELECT * FROM dbo.Orders WHERE Id = {id}";

			var reader = cmd.ExecuteReader();
			if (!reader.Read())
			{
				throw new EntityNotFound("Order", id);
			}

			var result = new Order { };

			result.Id = id;
			result.CustomerId = reader.GetInt32(1);
			result.Total = reader.GetDecimal(2);

			cmd = conn.CreateCommand();
			reader.Close();


			cmd.CommandText = $"SELECT * FROM dbo.OrderItems WHERE OrderId = {id}";
			reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				result.Items.Add(new orderItem
				{
					Code = reader.GetString(2),
					Description = reader.GetString(3),
					Price = Convert.ToSingle(reader.GetDecimal(4))
				});
			}

			conn.Close();
			return result;
		}

		public Customer GetCustomer(int customerId)
		{
			using (var conn = new SqlConnection(ConnectionString))
			{
				conn.Open();
				using (var cmd = conn.CreateCommand())
				{
					cmd.CommandText =
						$@"SELECT Country FROM Customers WHERE Id = {customerId}";
					var reader = cmd.ExecuteReader();
					if (!reader.Read())
						return null;
					return new Customer()
					{
						Id = customerId,
						Country = Convert.ToString(reader["Country"]),
					};
				}
			}
		}
	}

	class OrderOrderItem
	{
		public int OrderId { get; set; }
		public decimal Total { get; set; }

		public string Code { get; set; }
		public string Description { get; set; }
		public float Price { get; set; }
	}
}
