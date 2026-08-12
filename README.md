# E-Commerce RESTful API

A backend RESTful API for an E-Commerce application, built with **ASP.NET Core** and designed using **Clean Architecture** principles.

## Technologies

* C#
* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* LINQ
* JWT Authentication
* ASP.NET Core Identity
* Swagger / OpenAPI
* Repository Pattern
* Clean Architecture

## Architecture

The project is organized into four main layers:

* **API** — Handles HTTP requests, controllers, authentication, and API configuration.
* **Application** — Contains application services, business logic, DTOs, and interfaces.
* **Domain** — Contains entities and core domain models.
* **Infrastructure** — Handles database access, Entity Framework Core, repositories, and external implementations.

## Features

* User registration and authentication
* JWT-based authentication and authorization
* Product management
* Category management
* E-Commerce business operations
* Entity Framework Core database integration
* SQL Server database
* Repository Pattern
* RESTful API endpoints
* Swagger API documentation

## Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio or Visual Studio Code

### Installation

1. Clone the repository:

```bash
git clone https://github.com/Abdelmoneim-is/E_Commerce.git
```

2. Open the solution:

```text
E_Commerce.sln
```

3. Configure the SQL Server connection string in the application configuration.

4. Apply Entity Framework Core migrations if required.

5. Run the application.

6. Open Swagger to explore and test the API endpoints.

## Project Structure

```text
E_Commerce
│
├── E_Commerce.API
├── E_Commerce.Application
├── E_Commerce.Domain
├── E_Commerce.Infrastructure
│
└── E_Commerce.sln
```

## Purpose

This project was developed as a practical backend project to apply **ASP.NET Core Web API**, **Clean Architecture**, **Entity Framework Core**, **JWT Authentication**, and modern backend development practices.
