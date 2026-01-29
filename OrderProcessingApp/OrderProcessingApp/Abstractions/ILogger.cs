using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Abstractions;

public interface ILogger
{
	void LogInfo(string message);
	void LogError(string message, Exception ex);
}
