# ECommerce-API

ECommerceApp the application boasts a robust architecture and modern approach. 

## Table of Contents
- [Project Overview](#project-overview)
- [Key Features](#key-features)
- [Lessons Learned](#lessons-learned)
- [Getting Started](#getting-started)
- [Contributing](#contributing)

## Project Overview
<a name="project-overview"></a>
ECommerceApp is a practical demonstration of building a functional ASP.NET Core Web API, featuring:

1. **Models and Relationships:**
   - Defining data models for Users, Products, and Categories.
   - Establishing relationships between these entities.

2. **Entity Framework Core:**
   - Integrating Entity Framework Core for efficient database operations.

3. **Controllers and Actions:**
   - Creating controllers for CRUD (Create, Read, Update, Delete) operations.
   - Implementing actions for managing Users, Products, and Categories.

4. **Dependency Injection and Repository Pattern:**
   - Employing dependency injection for modularity and testability.
   - Utilizing the repository pattern for data access.

5. **Searching, Filtering:**
   - Implementing functionality for searching and filtering Products, Users, and Categories.
   

6. **Documentation:**
   - Documenting API endpoints for enhanced developer experience.
   - Leveraging Swagger UI for interactive API documentation.

7. **Error Handling:**
   - Handling errors and providing appropriate HTTP status codes.

## Key Features
<a name="key-features"></a>
- **Prodcut Management:**
   - Create, read, update, and delete Product.
   - Associate Product with Users and Categories.
  

- **User Management:**
   - Create, read Users.
   - Associate Users with Products.

- **Category Management:**
   - Create, read, update, and delete Categories.
   - Associate Categories with Products.

- **JWT Authentication:**
   - Implement secure user registration.
   - Generate authentication tokens for users.
   - Manage user roles securely.

- **Interactive API Documentation:**
   - Explore and test API endpoints using Swagger UI.

## Lessons Learned
<a name="lessons-learned"></a>
During the development of EcommerceApp, we've gained valuable insights into:

- Building a structured ASP.NET Core Web API application.
- Implementing data models and relationships.
- Leveraging Entity Framework Core for efficient database operations.
- Designing controllers and actions for CRUD operations.
- Promoting modularity and testability through dependency injection and the repository pattern.
- Implementing validation attributes with custom error messages on my Data Transfer Objects (DTOs).
- Handling invalid model state responses by reporting them as validation issues. This ensures that clients receive appropriate HTTP status codes and detailed information about validation errors.
- Enhancing the developer experience with interactive API documentation using Swagger UI.
- Adding searching and filtering functionality for improved data retrieval.
- Implementing secure user registration and authentication using JWT.
- Documenting API endpoints for better clarity and understanding.

## Getting Started
<a name="getting-started"></a>
To run this project locally, follow these steps:

1. Clone the repository: `git clone https://github.com/your-username/ECommerce-API.git`
2. Navigate to the project directory: `cd ECommerce-API`
3. Restore NuGet packages: `dotnet restore`
4. Run the application: `dotnet run`

## Contributing
<a name="contributing"></a>
Contributions to this project are welcome! If you find any issues or have ideas for improvements, please open an issue or create a pull request.