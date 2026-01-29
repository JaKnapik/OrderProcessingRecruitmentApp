using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Abstractions;

public interface IOrderService
{
	Task ProcessOrderAsync(int orderId);
}
