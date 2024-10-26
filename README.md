# WebApiSample
This API provides functionality to manage products in an e-commerce platform, following best practices in API development.

### Features:
- **Versioning**: Supports multiple API versions (v1, v2).
  - **v1**: Basic CRUD operations.
  - **v2**: Added pagination for listing products.
- **Pagination**: Implemented in v2 using `IPagedList` to improve performance on large datasets.
- **Partial Updates**: Supports updating only specific product fields (e.g., product description).
- **AutoMapper Integration**: Used to map between domain models and DTOs.
- **Clean Architecture**: Separation of concerns between Controller, Service, and Repository layers.
- **Mediator Pattern**: Used the Mediator pattern  (via [MediatR](https://github.com/jbogard/MediatR)) to manage interactions within the .NET controllers. This pattern keeps the controllers lightweight by delegating business logic to handlers, which improves code organization and testability.

### Technologies:
- ASP.NET Core
- AutoMapper
- X.PagedList for pagination
- Swagger for API documentation
- Entity Framework Core for data access

### Prerequisites

- VS 2022
- MSSQL Database up and running

### How To Run
1. Open `WebApiSample.sln` in VS 2022.
1. Change `ConnectionString` in `appsettings.json` if not using a local MSSQL database or if needed to change the DB name.
1. Open Package Manager Console -> Run `Update-Database`.
1. Run (F5).

### How To Run Unit Tests
1. Open `WebApiSample.sln` in VS 2022.
1. Go to Test -> Run All Tests.

### What next?
There is plenty of room for improvement, I know. I am happy to discuss more details.
