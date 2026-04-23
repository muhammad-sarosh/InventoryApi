# InventoryApi

ASP.NET Core Web API for product management with Entity Framework Core, SQL Server, JWT authentication, and role-based authorization.

## Overview

InventoryApi is a learning-focused backend project built to practice real API development with ASP.NET Core. It includes product CRUD operations, user registration and login, password hashing, JWT token generation, protected endpoints, and admin-only write access.

This project was used to practice:

- ASP.NET Core Web API
- controllers and routing
- services and dependency injection
- Entity Framework Core and migrations
- SQL Server LocalDB
- JWT authentication
- role-based authorization
- Swagger/OpenAPI testing

## Features

### Authentication
- User registration
- Password hashing using ASP.NET Core password hasher
- User login
- JWT token generation
- Authenticated user info endpoint (`/auth/me`)

### Products
- Get all products
- Get a product by id
- Create a product
- Update a product
- Delete a product

### Authorization
- Authenticated users can read product data
- Only users with the `Admin` role can create, update, or delete products

## Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server LocalDB
- Swagger / OpenAPI
- JWT Authentication
- Role-based Authorization

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
````

## Authentication Flow

### Register

A user registers with a username and password.
The password is hashed before being stored in the database.

### Login

A user logs in with a username and password.
If the credentials are valid, the API returns a JWT token.

### Authenticated Requests

The JWT token must be sent in the `Authorization` header as a Bearer token for protected endpoints.

### Role-Based Access

The JWT includes the user's role claim.
Users with the `User` role can read product data.
Users with the `Admin` role can create, update, and delete products.

## API Endpoints

### Auth

#### `POST /auth/register`

Registers a new user.

**Responses**

* `200 OK` - registration succeeded
* `400 Bad Request` - invalid input or username already exists

#### `POST /auth/login`

Logs in a user and returns a JWT token.

**Responses**

* `200 OK` - login succeeded
* `401 Unauthorized` - invalid username or password

#### `GET /auth/me`

Returns the current authenticated user's id, username, and role from JWT claims.

**Authorization**

* Requires a valid JWT

**Responses**

* `200 OK`
* `401 Unauthorized`

### Products

#### `GET /products`

Returns all products.

**Authorization**

* Requires a valid JWT

**Responses**

* `200 OK`
* `401 Unauthorized`

#### `GET /products/{id}`

Returns a single product by id.

**Authorization**

* Requires a valid JWT

**Responses**

* `200 OK`
* `404 Not Found`
* `401 Unauthorized`

#### `POST /products`

Creates a product.

**Authorization**

* Requires `Admin` role

**Responses**

* `201 Created`
* `401 Unauthorized`
* `403 Forbidden`

#### `PUT /products/{id}`

Updates a product by id.

**Authorization**

* Requires `Admin` role

**Responses**

* `204 No Content`
* `404 Not Found`
* `401 Unauthorized`
* `403 Forbidden`

#### `DELETE /products/{id}`

Deletes a product by id.

**Authorization**

* Requires `Admin` role

**Responses**

* `204 No Content`
* `404 Not Found`
* `401 Unauthorized`
* `403 Forbidden`

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

Make sure `appsettings.json` contains a valid SQL Server LocalDB connection string, for example:

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

Swagger should open automatically when the API starts.
If not, open the local Swagger URL shown by the app.

## Using Swagger with JWT

1. Register a user with `POST /auth/register`
2. Log in with `POST /auth/login`
3. Copy the returned JWT token
4. Click the **Authorize** button in Swagger
5. Paste the token
6. Call protected endpoints

## Admin Access

Newly registered users are created with the default role:

```text
User
```

To test admin-only product write operations, update a user's role in the database manually.

Example SQL:

```sql
UPDATE Users
SET Role = 'Admin'
WHERE Username = 'your_username';
```

After changing the role, log in again to generate a new token with the updated role claim.

## Database

This project uses Entity Framework Core with SQL Server LocalDB.

Current tables:

* `Products`
* `Users`
* `__EFMigrationsHistory`

Products are stored through EF Core.
Users are stored with hashed passwords and role information.

## Notes

* JWT tokens are signed using a secret key from `appsettings.json`
* Passwords are never stored in plain text
* Product read endpoints require authentication
* Product write endpoints require admin authorization

## License

This project is for learning and portfolio purposes.
