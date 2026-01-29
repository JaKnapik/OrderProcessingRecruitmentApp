using OrderProcessingApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Logging;

public class ConsoleLogger : ILogger
{
	public void LogError(string message, Exception ex)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"{DateTime.UtcNow.ToString()}: {message} \n {ex}");
		Console.ResetColor();
	}

	public void LogInfo(string message)
	{
		Console.WriteLine($"{DateTime.UtcNow.ToString()}: {message}");
	}
}
