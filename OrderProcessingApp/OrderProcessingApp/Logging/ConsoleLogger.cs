using Microsoft.Extensions.Configuration;
using OrderProcessingApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Logging;

public class ConsoleLogger : ILogger
{
	private readonly string _configuredLogLevel;

	public ConsoleLogger(IConfiguration configuration)
	{
		_configuredLogLevel = configuration["Logging:LogLevel"] ?? "Info";
	}
	public void LogError(string message, Exception ex)
	{
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine($"{DateTime.UtcNow.ToString()}: {message} \n {ex}");
		Console.ResetColor();
	}

	public void LogInfo(string message)
	{
		if (_configuredLogLevel.Equals("Error", StringComparison.OrdinalIgnoreCase))
		{
			return;
		}
		Console.WriteLine($"{DateTime.UtcNow.ToString()}: {message}");
	}
}
