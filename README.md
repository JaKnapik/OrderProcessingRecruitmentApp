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
2. Restore dependencies: dotnet restore.
3. Run the application: dotnet run.

## Completed bonus tasks
* Asynchronous processing
* Add Order (CRUD)
* IOrderValidator
* Unit Tests
* Configuration via appsettings.JSON
* Notification Service