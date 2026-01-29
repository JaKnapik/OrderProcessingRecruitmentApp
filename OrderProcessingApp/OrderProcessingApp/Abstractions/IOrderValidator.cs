using System;
using System.Collections.Generic;
using System.Text;

namespace OrderProcessingApp.Abstractions;

public interface IOrderValidator
{
	bool IsValid(int orderId);
}
