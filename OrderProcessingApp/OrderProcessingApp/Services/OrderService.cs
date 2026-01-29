using OrderProcessingApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Services;

public class OrderService : IOrderService
{
	private readonly IOrderRepository _orderRepository;
	private readonly ILogger _logger;
	private readonly IOrderValidator _orderValidator;
	private readonly INotificationService _notificationService;

	public OrderService(IOrderRepository orderRepository, ILogger logger, IOrderValidator orderValidator, INotificationService notificationService)
	{
		_orderRepository = orderRepository;
		_logger = logger;
		_orderValidator = orderValidator;
		_notificationService = notificationService;
	}

	public async Task ProcessOrderAsync(int orderId)
	{
		_logger.LogInfo($"Started processing of order with ID: {orderId}");
		try
		{
			if (_orderValidator.IsValid(orderId))
			{
				var order = await _orderRepository.GetOrder(orderId);
				_logger.LogInfo($"Successfully processed order with ID: {orderId}");
				_notificationService.Send($"Successfully processed order with ID: {orderId}");
			}
			else
			{
				throw new ArgumentException("Order ID must be greater than zero.");
			}
			
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
