using Microsoft.Extensions.Configuration;
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
		var baseDirectory = AppContext.BaseDirectory;

		IConfiguration configuration = new ConfigurationBuilder()
			.SetBasePath(baseDirectory)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.Build();

		ILogger logger = new ConsoleLogger(configuration);
		IOrderRepository repository = new OrderRepository();
		IOrderValidator orderValidator = new OrderValidator();
		INotificationService notificationService = new NotificationService();
		IOrderService orderService = new OrderService(repository, logger, orderValidator, notificationService);
		return (orderService, logger);
	}
}
