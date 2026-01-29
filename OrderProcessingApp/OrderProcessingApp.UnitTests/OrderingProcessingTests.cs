using Moq;
using OrderProcessingApp.Abstractions;
using OrderProcessingApp.Data;
using OrderProcessingApp.Services;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace OrderProcessingApp.UnitTests;

public class OrderingProcessingTests
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<ILogger> _loggerMock;
    private readonly Mock<INotificationService> _notificationMock;
	private readonly Mock<IOrderValidator> _validatorMock;
    private readonly OrderService _orderService;
	private readonly int invalidOrderId = -1;
	private readonly int validAndExistingOrderId = 1;
	
	private readonly int validAndNotExistingOrderId = 6;

	public OrderingProcessingTests()
	{
		_repositoryMock = new Mock<IOrderRepository>();
		_loggerMock = new Mock<ILogger>();
		_notificationMock = new Mock<INotificationService>();
		_validatorMock = new Mock<IOrderValidator>();

		_validatorMock.Setup(v => v.IsValid(It.Is<int>(id => id > 0))).Returns(true);

		_validatorMock.Setup(v => v.IsValid(It.Is<int>(id => id <= 0))).Returns(false);

		_orderService = new OrderService(
			_repositoryMock.Object,
			_loggerMock.Object,
			_validatorMock.Object,
			_notificationMock.Object);
	}
	[Fact]
    public async Task ProcessOrderAsync_ShouldLogArgumentExceptionErrorAndNotSendNotification_WhenOrderIdLessThanOne()
    {
        //Arange
        _repositoryMock.Setup(r => r.GetOrder(invalidOrderId)).ThrowsAsync(new ArgumentException("Order ID must be greater than zero."));

		//Act
        await _orderService.ProcessOrderAsync(invalidOrderId);

		//Assert
		_loggerMock.Verify(l => l.LogError(
			It.Is<string>(s => s.Contains("Order ID must be greater than zero")),
			It.IsAny<ArgumentException>()),
			Times.Once);
		_notificationMock.Verify(n => n.Send(It.IsAny<string>()), Times.Never);
	}

	[Fact]
	public async Task ProcessOrderAsync_ShouldReturnLogWithOrderId_WhenOrderIdIsValidAndExists()
	{
		string expectedDescriptionForOrderIdEqualToOne = $"Successfully processed order with ID: {validAndExistingOrderId}";
		//Arange
		_repositoryMock.Setup(r => r.GetOrder(validAndExistingOrderId)).ReturnsAsync(expectedDescriptionForOrderIdEqualToOne);

		//Act
		await _orderService.ProcessOrderAsync(validAndExistingOrderId);

		//Assert
		_loggerMock.Verify(l => l.LogInfo(
		It.Is<string>(s => s.Contains($"Successfully processed order with ID: {validAndExistingOrderId}"))),
		Times.Once);

		_notificationMock.Verify(n => n.Send(
			It.Is<string>(s => s.Contains(expectedDescriptionForOrderIdEqualToOne))),
			Times.Once);

		_loggerMock.Verify(l => l.LogError(It.IsAny<string>(), It.IsAny<Exception>()),
			Times.Never);
	}

	[Fact]
	public async Task ProcessOrderAsync_ShouldLogKeyNotFoundException_WhenOrderIdIsValidAndDoesNotExist()
	{
		//Arange
		string errorMessage = $"Order with ID {validAndNotExistingOrderId} was not found";
		_repositoryMock.Setup(r => r.GetOrder(validAndNotExistingOrderId))
		.ThrowsAsync(new KeyNotFoundException(errorMessage));

		//Act
		await _orderService.ProcessOrderAsync(validAndNotExistingOrderId);

		//Assert
		_loggerMock.Verify(l => l.LogError(
		It.Is<string>(s => s.Contains(errorMessage)),
		It.IsAny<KeyNotFoundException>()),
		Times.Once);

		// Sprawdzamy, czy powiadomienie NIE zostało wysłane (bo był błąd)
		_notificationMock.Verify(n => n.Send(It.IsAny<string>()),
			Times.Never);
	}
}
