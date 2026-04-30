# InventoryApi

ASP.NET Core Web API for inventory management with JWT authentication, role-based authorization, Azure SQL, Azure App Service deployment, and a separate frontend client.

## Overview

InventoryApi is a backend project built with ASP.NET Core Web API to practice real API development and deployment. It includes:

- user registration and login
- password hashing
- JWT token generation and validation
- protected endpoints
- role-based authorization
- product CRUD operations
- Entity Framework Core migrations
- Azure SQL database
- Azure App Service deployment
- CORS configuration for a separate frontend

This project was built to practice:

- ASP.NET Core Web API
- controllers and routing
- services and dependency injection
- Entity Framework Core and migrations
- SQL Server / Azure SQL
- JWT authentication
- role-based authorization
- deployment to Azure
- frontend/backend integration

## Live Demo

### Backend API
`https://inventoryapi-c9c8d2erf9fyfvec.uaenorth-01.azurewebsites.net`

### Frontend
`https://stockpad.vercel.app/`

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

### Deployment
- Backend deployed to Azure App Service
- Database deployed to Azure SQL
- Frontend deployed separately
- CORS enabled for frontend/backend communication

## Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / Azure SQL
- JWT Authentication
- Role-based Authorization
- Swagger / OpenAPI

### Hosting
- Azure App Service
- Azure SQL Database

### Frontend
- React
- Tailwind CSS
- Vite

## Project Structure

```text
InventoryApi
├── Controllers
│   ├── AuthController.cs
│   └── ProductsController.cs
├── Services
│   ├── AuthService.cs
│   └── ProductsService.cs
├── Migrations
├── InventoryDbContext.cs
├── Product.cs
├── ProductDto.cs
├── RegisterUserDto.cs
├── LoginUserDto.cs
├── User.cs
├── Program.cs
├── appsettings.json
└── appsettings.Development.example.json
````

## Authentication Flow

### Register

A user registers with a username and password.
The password is hashed before being stored in the database.

### Login

A user logs in with a username and password.
If the credentials are valid, the API returns a JWT token.

### Authenticated Requests

Protected endpoints require a JWT token in the `Authorization` header:

```http
Authorization: Bearer <your_token>
```

### Role-Based Access

The JWT includes the user's role claim.

* `User` can read product data
* `Admin` can create, update, and delete products

## API Endpoints

### Auth

#### `POST /auth/register`

Registers a new user.

**Responses**

* `200 OK`
* `400 Bad Request`

#### `POST /auth/login`

Logs in a user and returns a JWT token.

**Responses**

* `200 OK`
* `401 Unauthorized`

#### `GET /auth/me`

Returns the current authenticated user's id, username, and role.

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

## Running Locally

### 1. Clone the repository

```bash
git clone <your-repo-url>
cd InventoryApi
```

### 2. Restore packages

```bash
dotnet restore
```

### 3. Configure local settings

Create or update `appsettings.Development.json` with your local development settings.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=InventoryApiDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your-local-development-secret-key",
    "Issuer": "InventoryApi",
    "Audience": "InventoryApiUsers"
  }
}
```

### 4. Apply migrations

```powershell
Update-Database
```

Or:

```bash
dotnet ef database update
```

### 5. Run the API

```bash
dotnet run
```

Or run it from Visual Studio.

### 6. Open Swagger locally

Swagger is available in local development mode.

## Running in Production

The deployed Azure app uses environment variables for configuration, including:

* `ConnectionStrings__DefaultConnection`
* `Jwt__Key`
* `Jwt__Issuer`
* `Jwt__Audience`
* `ASPNETCORE_ENVIRONMENT=Production`

In production:

* the root URL may return `404` if no `/` route is mapped
* Swagger is not exposed by default
* the API is intended to be used by the deployed frontend or direct API requests

## Frontend Integration

The frontend is hosted separately and communicates with the API over HTTP requests.

Important points:

* the frontend stores the JWT after login
* the JWT is sent in the `Authorization` header for protected endpoints
* CORS is enabled on the backend for allowed frontend origins

## Example Authenticated Request

```bash
curl https://inventoryapi-c9c8d2erf9fyfvec.uaenorth-01.azurewebsites.net/Auth/me \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## Admin Access

Newly registered users are created with the default role:

```text
User
```

To test admin-only endpoints, update a user's role in the database manually:

```sql
UPDATE Users
SET Role = 'Admin'
WHERE Username = 'your_username';
```

After changing the role, log in again to generate a new token with the updated role claim.

## Database

This project uses Entity Framework Core with SQL Server.

Development can use LocalDB.
Production uses Azure SQL Database.

Current tables include:

* `Products`
* `Users`
* `__EFMigrationsHistory`

Products are stored through EF Core.
Users are stored with hashed passwords and role information.

## Notes

* JWT tokens are signed using a secret key from configuration
* Passwords are never stored in plain text
* Product read endpoints require authentication
* Product write endpoints require admin authorization

## License

This project is for learning and portfolio purposes.

