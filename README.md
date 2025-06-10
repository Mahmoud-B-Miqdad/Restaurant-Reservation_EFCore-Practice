# 🍽️ Restaurant Reservation System API

![.NET](https://img.shields.io/badge/.NET-8-512BD4?logo=dotnet)
![EF Core](https://img.shields.io/badge/EF_Core-7.0-green)
![JWT Auth](https://img.shields.io/badge/JWT-Auth-orange)
![Swagger](https://img.shields.io/badge/Swagger-Docs-85EA2D)

A comprehensive restaurant reservation management system API built with ASP.NET Core Web API and Entity Framework Core.

## 🌟 Features

- **Full CRUD operations** for all entities (Restaurants, Reservations, Orders, etc.)
- **JWT Authentication** for secure endpoints
- **Comprehensive documentation** with Swagger
- **Advanced querying** with:
  - Database views
  - Stored procedures
  - Custom functions
- **Validation** and error handling
- **Postman test collection** for easy testing

## 🏗️ Project Structure

```
Restaurant-Reservation/
├── RestaurantReservation.API/        # Web API project
├── RestaurantReservation.Db/         # Data access layer
│   ├── Models/                       # Entity models
│   ├── Repositories/                 # Repository classes
│   ├── Migrations/                   # Database migrations
│   └── RestaurantReservationDbContext.cs
└── RestaurantReservation.Domain/            
```

## 🔌 API Endpoints

### 📍 Restaurants
- `GET /api/restaurants` - List all restaurants
- `GET /api/restaurants/{id}` - Get restaurant by ID
- `POST /api/restaurants` - Create new restaurant
- `PUT /api/restaurants/{id}` - Update restaurant
- `DELETE /api/restaurants/{id}` - Delete restaurant

### 📅 Reservations
- `GET /api/reservations` - List all reservations
- `GET /api/reservations/customer/{customerId}` - Get reservations by customer
- `GET /api/reservations/{id}/orders` - Get orders for reservation
- `GET /api/reservations/{id}/menu-items` - Get menu items for reservation
- `POST /api/reservations` - Create new reservation

### 👨‍🍳 Employees
- `GET /api/employees/managers` - List all managers
- `GET /api/employees/{id}/average-order-amount` - Get average order amount for employee
- `POST /api/employees` - Create new employee

### 🍽️ Menu Items
- `GET /api/menu-items` - List all menu items
- `POST /api/menu-items` - Create new menu item

### 🔐 Authentication
- `POST /api/auth/login` - Get JWT token

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Postman (for testing)

### Running the API
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run migrations:
   ```bash
   dotnet ef database update
   ```
4. Start the API:
   ```bash
   dotnet run
   ```

## 📚 API Documentation

Swagger documentation is available at `/swagger` when running the API.

## 🧪 Testing

Import the provided Postman collection to test all endpoints.

Sample test cases:
- Create reservation
- Get manager list
- Calculate average order amount
- JWT authentication flow

## 📊 Database Schema

The database includes tables for:
- Restaurants
- Reservations
- Orders
- MenuItems
- Employees
- Customers
- Tables
- OrderItems
