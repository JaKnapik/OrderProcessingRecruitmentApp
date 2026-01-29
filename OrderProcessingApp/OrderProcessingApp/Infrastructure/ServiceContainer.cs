using OrderProcessingApp.Abstractions;
using OrderProcessingApp.Data;
using OrderProcessingApp.Logging;
using OrderProcessingApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Infrastructure;

public static class ServiceContainer
{
	public static (IOrderService OrderService, ILogger Logger) CreateServices()
	{
		ILogger logger = new ConsoleLogger();
		IOrderRepository repository = new OrderRepository();

		IOrderService orderService = new OrderService(repository, logger);

		return (orderService, logger);
	}
}
