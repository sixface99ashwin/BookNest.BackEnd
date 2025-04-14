# 📚 BookNest.BackEnd

BookNest is a scalable and modular backend Web API built using ASP.NET Core. The project is currently under development and aims to serve as the foundation for a full-fledged e-commerce-like book store platform.

This repository demonstrates best practices in clean architecture, layered design, dependency injection, and API modularization. It’s ideal for learning, contribution, or showcasing backend development capabilities in .NET.

---

## 🏗️ Project Architecture

The project follows a clean and organized folder structure with proper separation of concerns:

### 🔹 `Controllers/`
- Handles all HTTP endpoints.
- Light-weight, only responsible for delegating calls to the business layer.

### 🔹 `BusinessLogic/`
- Contains all application logic.
- Divided into `Interfaces` and `Implementations` to ensure abstraction and loose coupling.

### 🔹 `DataAccess/`
- Encapsulates database interaction logic.
- Also uses interfaces and repository pattern for extensibility.

### 🔹 `Common/`
- `Exceptions/`: Custom exceptions for specific error scenarios.
- `Helpers/`:  
  - One helper class reads query templates from JSON and replaces dynamic values.  
  - Another helper builds queries dynamically at runtime.
- `Constants/`: Stores static values used throughout the project.

### 🔹 `ElasticQueries/`
- Houses Elasticsearch query templates in `.json` format.
- Useful for reusable and readable query management.

### 🔹 `HttpClient/`
- Wrapper around HTTP communication to easily consume external APIs.

### 🔹 `Models/`
- `Domain/`: Represents database entity models.
- `Configure/`: Classes to bind `appsettings.json` config values.
- `DTOs/`: Segregated into `Request/` and `Response/` objects for clear API contract handling.

### 🔹 `Middleware/`
- Contains custom middleware for global exception handling.
- Ensures consistent error responses throughout the API.

---

## 🧰 Tech Stack

- **Framework:** ASP.NET Core Web API (.NET 8)
- **Language:** C#
- **Design Patterns:** Repository Pattern, Dependency Injection, Middleware
- **Tools:**  
  - SQL Database (planned)  
  - Elasticsearch (query files already structured)  
  - HttpClient for external API calls  
  - Postman (for testing - collection to be added)

---

## 🔌 Features & Highlights

- ✅ Layered architecture with separation of concerns.
- ✅ Scoped DI registration for service interfaces and implementations.
- ✅ Dynamic query generation with parameter replacement.
- ✅ Centralized error handling using middleware.
- ✅ Clean code with domain-driven principles.
- 🚧 More features like Authentication, Caching, and Unit Tests are planned.

---

## 🚀 Getting Started

> **Note:** The project is still under development. Not all endpoints and features are available yet.

### Prerequisites:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio or VS Code
- (Optional) Elasticsearch for testing search queries

### To Run:
```bash
git clone https://github.com/sixface99ashwin/BookNest.BackEnd.git
cd BookNest.BackEnd
dotnet restore
dotnet run
