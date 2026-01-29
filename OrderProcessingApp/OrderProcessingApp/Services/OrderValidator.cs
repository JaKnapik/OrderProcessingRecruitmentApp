using OrderProcessingApp.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Services;

public class OrderValidator: IOrderValidator
{
	public bool IsValid(int orderId)
	{
		return orderId > 0;
	}
}
