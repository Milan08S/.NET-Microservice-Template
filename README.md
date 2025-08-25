# .NET Clean Architecture Microservice Template

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](#) A robust and scalable template for building .NET microservices, designed with Clean Architecture principles and ready for containerization with Docker.

This template provides a solid foundation for creating new services, ensuring consistency, maintainability, and best practices across your projects. It's pre-configured to be used as a custom `dotnet new` template.

---
## Key Features ✨

* **.NET 9:** Built on the latest version of the .NET platform.
* **Clean Architecture:** Follows a strict separation of concerns with four primary projects:
    * `Domain`: Core entities and business logic interfaces.
    * `Application`: Application-specific business rules and services.
    * `Infrastructure`: Data access, external services, and implementation details.
    * `Api`: Exposes the application functionality through a RESTful API.
* **Generic Repository Pattern:** Includes a generic repository for common CRUD operations to reduce boilerplate code, with a clear pattern for extension.
* **Entity Framework Core:** Pre-configured for data access using a Code-First approach.
* **PostgreSQL Ready:** Configured to work with a PostgreSQL database.
* **Docker Support:** Comes with a multi-stage `Dockerfile` for building optimized, production-ready container images.
* **`dotnet new` Template:** Easily install and reuse this template for any new microservice with a single command.

---
## Project Structure 🏗️

The solution is organized into distinct layers, ensuring a clean separation of concerns.

```
/YourSolutionName.sln
│
├── src/
│   ├── YourSolutionName.Api/
│   ├── YourSolutionName.Application/
│   ├── YourSolutionName.Domain/
│   └── YourSolutionName.Infrastructure/
│
├── tests/
│   └── YourSolutionName.Application.Tests/
│
├── .template.config/
│   └── template.json
│
├── Dockerfile
└── .dockerignore
```

---
## Getting Started

Follow these instructions to install the template and create your first microservice from it.

### Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)
* A running PostgreSQL instance.

### Installation (Using as a Template)

1.  **Clone the repository:**
    ```bash
    git clone [https://github.com/your-username/your-repo-name.git](https://github.com/your-username/your-repo-name.git)
    cd your-repo-name
    ```

2.  **Install the template:**
    Run the following command from the root directory of the repository to install it into your local .NET template hive.
    ```bash
    dotnet new install .
    ```

3.  **Create your new microservice:**
    You can now use the template's short name (`mimicro`) to create a new project.
    ```bash
    # Navigate to a different folder where you want to create your new project
    cd ../
    
    # Use the template
    dotnet new mimicro -o MyNewAwesomeService
    ```
    This will create a new folder `MyNewAwesomeService` with all projects, files, and namespaces correctly renamed.

---
## Usage

Once you have created a new project from the template, you can run it locally or using Docker.

### Running with the .NET CLI

1.  Navigate to the API project directory:
    ```bash
    cd MyNewAwesomeService/src/MyNewAwesomeService.Api
    ```
2.  Run the application:
    ```bash
    dotnet run
    ```
    The API will be available at the URLs specified in the `launchSettings.json` file.

### Running with Docker 🐳

1.  **Build the Docker image:**
    From the root of your new project (`MyNewAwesomeService`), run:
    ```bash
    docker build -t mynewawesomeservice .
    ```

2.  **Run the Docker container:**
    Execute the `docker run` command, making sure to provide the necessary environment variables for the environment and the database connection string.

    ```bash
    docker run -d -p 8081:8080 --name mynewawesomeservice-app \
      -e "ASPNETCORE_ENVIRONMENT=Development" \
      -e "ConnectionStrings__DefaultConnection=Server=host.docker.internal;Port=5432;Database=MyDb;User Id=postgres;Password=your_password;" \
      mynewawesomeservice
    ```
    > **Note:** We use `host.docker.internal` in the connection string to allow the container to connect to the PostgreSQL database running on your host machine.

3.  **Access the API:**
    Your API is now running and accessible at `http://localhost:8081`. You can view the interactive documentation at `http://localhost:8081/swagger`.

---
## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
