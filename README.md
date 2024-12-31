
# PassIn Solution

## Overview
The **PassIn Solution** is a backend Web API designed for managing events and their participants. It allows users to create, list, and manage both events and participants. The solution is built using .NET and follows the principles of Clean Architecture to ensure maintainability and scalability.

## Key Features
- **Event Management**: Create and list events.
- **Participant Management**: Register and list participants for events.
- **SQLite Database**: Simplified database setup for ease of development and testing.
- **Unit Testing**: Comprehensive unit tests using xUnit.

## Solution Structure
The solution consists of six projects:

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
   - Configures SQLite as the database provider.
   - Manages migrations and entity configurations.

6. **PassIn.UnitTests**
   - Contains unit tests for the application.
   - Uses xUnit for test cases to ensure the reliability of business logic and other components.

## Technologies Used
- **.NET 8**: Backend framework for Web API development.
- **Entity Framework Core**: ORM for database interactions.
- **SQLite**: Lightweight database for development purposes.
- **xUnit**: Testing framework for unit tests.
- **Clean Architecture**: Ensures a clear separation of concerns and maintainability.

## Getting Started

### Prerequisites
- .NET 8 SDK
- Visual Studio 2022 or any IDE supporting .NET development

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
4. Apply migrations to set up the database:
   ```bash
   dotnet ef database update --project PassIn.Infrastructure
   ```
5. Run the application:
   ```bash
   dotnet run --project PassIn.Api
   ```

### Running Tests
1. Navigate to the `PassIn.UnitTests` project directory.
2. Run the tests:
   ```bash
   dotnet test
   ```

## Project Highlights
- **SQLite Database**: Chosen for simplicity and to avoid making the database a focal point of the application. Migrations and configurations are handled via EF Core.
- **Clean Architecture**: Ensures separation of concerns by organizing the solution into clear layers.
- **xUnit Tests**: Validates the core functionality of the application and ensures the integrity of business logic.

---
