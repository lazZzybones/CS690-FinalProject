# Freelance Management System v2.0.0

A modular console-based application to help freelancers organize projects, clients, invoices, and deadlines.

## Version 2.0.0 - What's New

### New Features (5 FR)
- **FR4: View Upcoming Deadlines** - See all deadlines sorted by date
- **FR5: Create Invoice** - Generate invoices for projects
- **FR6: View Invoice Status** - Track paid and unpaid invoices
- **FR7: Add New Client** - Manage client database
- **FR8: View All Clients** - View all clients
- **FR9: Update Project Status** - Change project status (BONUS)

### Technical Improvements
- ✅ **Modular Architecture** - Organized into Models, Services, Data, UI
- ✅ **Unit Tests** - Comprehensive test coverage with xUnit
- ✅ **Separation of Concerns** - Clear layer separation
- ✅ **3 Distinct Modules**:
  - **ClientService** - Client management logic
  - **ProjectService** - Project management logic
  - **InvoiceService** - Invoice management logic

## Project Structure

```
FreelanceManagementSystem/
├── Models/                  # Data models
│   ├── Project.cs
│   ├── Client.cs
│   ├── Invoice.cs
│   └── Deadline.cs
├── Services/                # Business logic (3 MODULES)
│   ├── ClientService.cs
│   ├── ProjectService.cs
│   ├── InvoiceService.cs
│   └── DeadlineService.cs
├── Data/                    # Data persistence
│   └── DataManager.cs
├── UI/                      # User interface
│   └── ConsoleUI.cs
├── Tests/                   # Unit tests
│   ├── ClientServiceTests.cs
│   ├── ProjectServiceTests.cs
│   └── InvoiceServiceTests.cs
├── Program.cs               # Entry point
└── FreelanceManagementSystem.csproj
```

## Technology Stack

- **.NET 10.0**
- **C#**
- **xUnit** for testing
- **JSON** for data persistence

## Quick Start

### Run the Application

```bash
cd FreelanceManagementSystem-v2
dotnet run
```

### Run Tests

```bash
cd Tests
dotnet test
```

## Features Summary

### From v1.0.0
- FR1: Create New Project
- FR2: View All Projects  
- FR3: Add Deadline to Project

### New in v2.0.0
- FR4: View Upcoming Deadlines
- FR5: Create Invoice
- FR6: View Invoice Status
- FR7: Add New Client
- FR8: View All Clients
- FR9: Update Project Status

## Data Storage

- `projects.json` - Project data
- `clients.json` - Client data
- `invoices.json` - Invoice data

## Testing

All 3 modules have comprehensive unit tests:

- **ClientServiceTests** - 7 tests
- **ProjectServiceTests** - 8 tests
- **InvoiceServiceTests** - 10 tests

Run tests with: `dotnet test`

## Documentation

- [User Documentation](https://github.com/lazZzybones/CS690-FinalProject/wiki/User-Documentation)
- [Development Documentation](https://github.com/lazZzybones/CS690-FinalProject/wiki/Development-Documentation)
- [Deployment Documentation](https://github.com/lazZzybones/CS690-FinalProject/wiki/Deployment-Documentation)

## License

CS690 Final Project

## Author

Created for CS690 coursework - Iteration 2
