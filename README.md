# InventoryApi

A learning-focused ASP.NET Core Web API project for managing products with JWT-based authentication and role-based authorization.

## Overview

InventoryApi is a backend API built with ASP.NET Core Web API, Entity Framework Core, and SQL Server LocalDB. It supports product CRUD operations, user registration and login, password hashing, JWT token generation, protected endpoints, and admin-only write access for product management.

This project was built to practice real backend concepts including:

- controllers and routing
- services and dependency injection
- Entity Framework Core and migrations
- authentication vs authorization
- password hashing
- JWT token generation and validation
- role-based access control

---

## Features

### Authentication
- User registration
- Password hashing using ASP.NET Core password hasher
- User login
- JWT token generation
- Authenticated user info endpoint (`/auth/me`)

### Product API
- Get all products
- Get a product by id
- Create a product
- Update a product
- Delete a product

### Authorization
- Authenticated users can read product data
- Only users with the `Admin` role can create, update, or delete products

---

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI
- JWT Authentication
- Role-based Authorization

---

## Project Structure

```text
InventoryApi
├── Controllers
│   ├── AuthController.cs
│   └── ProductsController.cs
├── Services
│   ├── AuthService.cs
│   └── ProductsService.cs
├── Product.cs
├── ProductDto.cs
├── RegisterUserDto.cs
├── LoginUserDto.cs
├── User.cs
├── InventoryDbContext.cs
├── Program.cs
├── appsettings.json
└── Migrations
```
---

## Authentication Flow

### Register

A user registers with a username and password.

The password is never stored directly. It is hashed before being saved in the database.

### Login

A user logs in with their username and password.

If the credentials are correct, the API returns a JWT token.

### Authorized Requests

The token can then be used to access protected endpoints by sending it in the `Authorization` header as a Bearer token.

### Role-Based Access

The JWT includes the user's role claim. Endpoints marked with admin-only authorization require a token belonging to a user whose role is `Admin`.

---

## API Endpoints

### Auth

#### `POST /auth/register`

Register a new user.

#### `POST /auth/login`

Log in and receive a JWT token.

#### `GET /auth/me`

Get the current authenticated user's id, username, and role from the JWT claims.

---

### Products

#### `GET /products`

Get all products.
Requires authentication.

#### `GET /products/{id}`

Get a product by id.
Requires authentication.

#### `POST /products`

Create a product.
Requires `Admin` role.

#### `PUT /products/{id}`

Update a product.
Requires `Admin` role.

#### `DELETE /products/{id}`

Delete a product.
Requires `Admin` role.

---

## How to Run

### 1. Clone the repository

```bash
git clone <your-repo-url>
cd InventoryApi
```

### 2. Restore packages

```bash
dotnet restore
```

### 3. Check the connection string

Make sure `appsettings.json` contains a valid LocalDB connection string, for example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=InventoryApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 4. Apply migrations

```powershell
Update-Database
```

Or with the .NET CLI:

```bash
dotnet ef database update
```

### 5. Run the project

```bash
dotnet run
```

Or run it from Visual Studio.

### 6. Open Swagger

When the API starts, Swagger should open automatically. If not, open the Swagger URL shown in the browser or terminal.

---

## Using Swagger with JWT

1. Register a user with `POST /auth/register`
2. Log in with `POST /auth/login`
3. Copy the returned JWT token
4. Click the **Authorize** button in Swagger
5. Paste the token
6. Call protected endpoints

---

## Admin Access

By default, registered users are created with the role:

```text
User
```

To test admin-only product write operations, update a user’s role in the database manually.

Example SQL:

```sql
UPDATE Users
SET Role = 'Admin'
WHERE Username = 'your_username';
```

After changing the role, log in again to get a new token with the updated role claim.

---

## Database

This project uses Entity Framework Core with SQL Server LocalDB.

Current tables include:

* `Products`
* `Users`
* `__EFMigrationsHistory`

Products are stored in the database through EF Core.
Users are stored with hashed passwords and role information.

---

## Notes

* JWT tokens are signed using a secret key from `appsettings.json`
* Passwords are hashed before storage
* Product read endpoints require authentication
* Product write endpoints require admin authorization

---

## Future Improvements

Possible next improvements for this project:

* separate request/response DTOs more cleanly
* stronger validation and error responses
* refresh tokens
* token expiration/refresh flow
* automatic admin seeding
* deployment to Azure or Render
* unit and integration tests

---

## Learning Goals Covered

This project covers practical backend concepts such as:

* REST API design
* controllers and routes
* services and dependency injection
* EF Core setup and migrations
* JWT authentication
* password hashing
* authorization with roles
* protected endpoints

---

## License

This project is for portfolio purposes.
