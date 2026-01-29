using OrderProcessingApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Services;

public class NotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"[NOTIFICATION] Sending: {message}");
    }
}
