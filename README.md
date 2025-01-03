
# PassIn Solution

## Overview
The **PassIn Solution** is a backend Web API designed for managing events and their participants. It allows users to create, list, and manage both events and participants. The solution is built using .NET and follows the principles of Clean Architecture to ensure maintainability and scalability.

## Key Features
- **Event Management**: Create and list events.
- **Participant Management**: Register and list participants for events.
- **SQL Server Database**: Simplified database setup for ease of development and testing.
- **Unit Testing**: Comprehensive unit tests using xUnit.
- **Integration Testing**: Includes integration tests using `mvc.testing`.
- **Mutation Testing**: Test coverage analysis using Stryker.

## Solution Structure
The solution consists of seven projects:

1. **PassIn.Api**
   - The main entry point for the application.
   - Hosts the Web API endpoints.
   - Configures middleware, dependency injection, and application settings.

2. **PassIn.Application**
   - Contains the core business logic.
   - Implements use cases for managing events and participants.
   - Interfaces for dependency inversion are defined here.

3. **PassIn.Communication**
   - Defines Data Transfer Objects (DTOs) and API contracts.
   - Facilitates communication between the API and the application layers.

4. **PassIn.Exceptions**
   - Centralized project for managing custom exceptions.
   - Provides consistent error handling across the application.

5. **PassIn.Infrastructure**
   - Handles database access using Entity Framework Core (EF Core).
   - Configures SQL Server as the database provider.
   - Manages migrations and entity configurations.

6. **PassIn.UnitTests**
   - Contains unit tests for the application.
   - Uses xUnit for test cases to ensure the reliability of business logic and other components.
   - Incorporates mutation testing with Stryker to validate test quality.

7. **PassIn.IntegrationTests**
   - Contains integration tests for the application.
   - Uses `mvc.testing` to validate end-to-end interactions within the application.

## Technologies Used
- **.NET 8**: Backend framework for Web API development.
- **Entity Framework Core**: ORM for database interactions.
- **SQL Server**: Database for development purposes.
- **Docker Compose**: Used to spin up a SQL Server container for running the API.
- **xUnit**: Testing framework for unit tests.
- **mvc.testing**: Framework for writing integration tests.
- **Stryker**: Mutation testing framework for evaluating test coverage and effectiveness.
- **Clean Architecture**: Ensures a clear separation of concerns and maintainability.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or any IDE supporting .NET development
- Docker and Docker Compose

### Setup Instructions
1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```
2. Navigate to the solution directory and open the `.sln` file in your IDE.
3. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
4. Start the SQL Server container using Docker Compose:
   ```bash
   docker-compose up -d
   ```
5. Apply migrations to set up the database:
   ```bash
   dotnet ef database update --project PassIn.Infrastructure --startup-project PassIn.Api
   ```
6. Run the application:
   ```bash
   dotnet run --project PassIn.Api
   ```

### Running Tests
1. Navigate to the `PassIn.UnitTests` project directory to run unit tests or the `PassIn.IntegrationTests` project directory for integration tests.
2. Run the tests:
   ```bash
   dotnet test
   ```
3. Run mutation tests with Stryker to evaluate test coverage:
   ```bash
   dotnet stryker
   ```

## Project Highlights
- **SQL Server Database**: Chosen for its robustness and scalability, running in a containerized environment using Docker Compose. Migrations and configurations are handled via EF Core.
- **Clean Architecture**: Ensures separation of concerns by organizing the solution into clear layers.
- **xUnit Tests**: Validates the core functionality of the application and ensures the integrity of business logic.
- **Integration Tests**: Ensures proper interaction between application components using `mvc.testing`.
- **Stryker Mutation Testing**: Enhances test quality by identifying weaknesses in test coverage and ensuring robustness.

---

