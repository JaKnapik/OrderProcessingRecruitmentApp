using OrderProcessingApp.Abstractions;
using OrderProcessingApp.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Data;

public class OrderRepository : IOrderRepository
{
	private readonly ConcurrentDictionary<int, Order> _orders;

	public OrderRepository()
	{
		_orders = new ConcurrentDictionary<int, Order>();
		_orders.TryAdd(1, new Order
		{
			Id = 1,
			Description = "Laptop"
		});
		_orders.TryAdd(2, new Order
		{
			Id = 2,
			Description = "Phone"
		});
	}
	public string GetOrder(int orderId)
	{
		if (orderId <= 0)
		{
			throw new ArgumentException("Order ID must be greater than zero.");
		}

		if (_orders.TryGetValue(orderId, out var orderName))
		{
			return orderName.Description;
		}

		throw new KeyNotFoundException($"Order with ID {orderId} was not found");
	}
}
