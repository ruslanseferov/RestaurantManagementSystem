# Restaurant Management System

A console-based restaurant management application built with **C#**, **Entity Framework Core**, and **SQL Server**. The system supports managing multiple restaurant branches, their tables, menus, and orders — with a statistics module for tracking revenue and sales performance.

---

## Features

- **Restaurant management** — add, update, and delete restaurant branches with unique branch codes
- **Table management** — assign tables to restaurants, track capacity and order count
- **Menu management** — manage food/drink items per restaurant, organized by category, with price and sales tracking
- **Order management** — create orders linked to a restaurant and table, with support for multiple order items and automatic total calculation
- **Statistics** — view per-restaurant status, revenue rankings across all branches, top-selling menu items per restaurant, and global best-sellers
- **Seed data** — database is automatically populated with sample data on the first run

---

## Tech Stack

| Technology | Purpose |
|---|---|
| C# / .NET | Application logic |
| Entity Framework Core 8 | ORM and database access |
| SQL Server Express | Database |
| Repository Pattern | Data access layer abstraction |
| Service Layer | Business logic separation |
| Async/Await | Asynchronous operations throughout |

---

## Project Structure

```
RestaurantManagementSystem/
├── Models/              # Entity classes (Restaurant, Table, MenuItem, Order, OrderItem)
├── Data/                # DbContext and seed data
├── Repositories/        # Repository interfaces and implementations
│   └── Interfaces/
├── Services/            # Business logic (RestaurantService, OrderService, StatisticsService, etc.)
├── UI/                  # Console menu screens for each module
└── Program.cs           # Entry point, dependency wiring, main loop
```

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server Express (or any SQL Server instance)
- [EF Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet): `dotnet tool install --global dotnet-ef`

---

## Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/ruslanseferov/RestaurantManagementSystem.git
   cd RestaurantManagementSystem
   ```

2. **Update the connection string**

   Open `Data/RestaurantContext.cs` and update the connection string to match your SQL Server instance:
   ```csharp
   optionsBuilder.UseSqlServer(
       "Server=YOUR_SERVER\\SQLEXPRESS;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;"
   );
   ```

3. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

The database will be seeded automatically on the first run.

---

## What I Learned

- Designing a relational database schema with multiple related entities (one-to-many, many-to-many via junction table)
- Implementing the **Repository Pattern** and **Service Layer** architecture to separate concerns
- Handling EF Core cascade delete conflicts with `DeleteBehavior.NoAction`
- Using **unique indexes** to enforce data integrity at the database level
- Storing price snapshots in `OrderItem` to preserve historical order data
- Writing **async/await** code throughout a console application
