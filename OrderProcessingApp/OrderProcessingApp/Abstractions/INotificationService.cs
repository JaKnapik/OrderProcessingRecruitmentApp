using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Abstractions;
public interface INotificationService
{
    void Send(string message);
}
