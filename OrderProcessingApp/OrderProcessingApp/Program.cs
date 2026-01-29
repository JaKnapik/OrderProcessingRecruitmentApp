using OrderProcessingApp.Abstractions;
using OrderProcessingApp.Data;
using OrderProcessingApp.Infrastructure;
using OrderProcessingApp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace OrderProcessingApp;

class Program
{
	static async Task Main(string[] args)
	{
		var (orderService, logger) = Infrastructure.ServiceContainer.CreateServices();
		Console.WriteLine("Order Processing System");

		Task[] tasks = new Task[4];
		tasks[0] = Task.Run(() => orderService.ProcessOrderAsync(1));
		tasks[1] = Task.Run(() => orderService.ProcessOrderAsync(2));
		tasks[2] = Task.Run(() => orderService.ProcessOrderAsync(-1));
		tasks[3] = Task.Run(() => orderService.AddOrderAsync(new Order
		{
			Id = 4,
			Description = "Tablet"
		}));

		await Task.WhenAll(tasks);

		Console.WriteLine("Processing complete.");
		logger.LogInfo("All orders processed.");
	}
}