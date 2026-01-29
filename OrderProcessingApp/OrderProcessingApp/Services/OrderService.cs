using OrderProcessingApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly ILogger _logger;

	public OrderService(IOrderRepository orderRepository, ILogger logger)
	{
		_orderRepository = orderRepository;
		_logger = logger;
	}

	public void ProcessOrder(int orderId)
	{
		_logger.LogInfo($"Started processing of order with ID: {orderId}");
		try
		{
			var order = _orderRepository.GetOrder(orderId);
			_logger.LogInfo($"Successfully processed order with ID: {orderId}");
		}
		catch (ArgumentException ex)
		{
			_logger.LogError("Order ID must be greater than zero.", ex);
		}
		catch (KeyNotFoundException ex)
		{
			_logger.LogError($"Order with ID {orderId} was not found", ex);
		}
		catch (Exception ex)
		{
			_logger.LogError("Exception occured", ex);
		}
	}
}
