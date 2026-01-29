using OrderProcessingApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Abstractions;

public interface IOrderRepository
{
	string GetOrder(int orderId);
}
