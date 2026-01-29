using OrderProcessingApp.Abstractions;
using OrderProcessingApp.Infrastructure;
using OrderProcessingApp.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;

namespace OrderProcessingApp;

class Program
{
	static void Main(string[] args)
	{
		var (orderService, logger) = Infrastructure.ServiceContainer.CreateServices();
		Console.WriteLine("Order Processing System");

		// Example: Simulate multiple threads processing orders
		Task[] tasks = new Task[3];
		tasks[0] = Task.Run(() => orderService.ProcessOrder(1));
		tasks[1] = Task.Run(() => orderService.ProcessOrder(2));
		tasks[2] = Task.Run(() => orderService.ProcessOrder(-1));

		Task.WaitAll(tasks);

		Console.WriteLine("Processing complete.");
		logger.LogInfo("All orders processed.");
	}
}