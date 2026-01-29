# Order processing system

## Overview
A small, modular, and thread-safe console application built with .NET 10 for asynchronous order processing. 
The solution demonstrates Dependency Injection, Logging and Error Handling while adhering to good software design practices.

## Technical Stack & Dependencies
* **Runtime**: .NET 10 
* **Concurrency**: ConcurrentDictionary<int, Order> for thread-safety
* **Configuration**: Microsoft.Extensions.Configuration for JSON-based settings.
* **Unit testing**: xUnit and Moq

## Architecture diagram

The application uses a Manual DI Container to manage lifecycle and Dependencies
```graph TD
    Main[Program.cs] --> SC[ServiceContainer]
    SC --> OS[OrderService]
    OS --> Repo[IOrderRepository]
    OS --> Log[ILogger]
    OS --> Val[IOrderValidator]
    OS --> Noti[INotificationService]
```

### Required NuGet packages
* Microsoft.Extensions.Configuration
* Microsoft.Extensions.Configuration.JSON
* Microsoft.Extensions.Configuration.FileExtensions
* Moq
* xunit
* Microsoft.NET.Test.Sdk

## How to Run
1. Ensure .NET 10 SDK is installed.
2. Clone the repository.
3. Go into the downloaded repository, then into the OrderProcessingApp/OrderProcessingApp folder
4. Launch terminal in that directory.
5. Restore dependencies: dotnet restore.
6. Run the application: dotnet run.

## Completed bonus tasks
* Asynchronous processing
* Add Order (CRUD)
* IOrderValidator
* Unit Tests
* Configuration via appsettings.JSON
* Notification Service
