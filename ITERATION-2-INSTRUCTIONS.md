# Iteration 2 (v2.0.0) - Complete Instructions

## What's Been Created

### ✅ Code Structure (Modular)

```
FreelanceManagementSystem-v2/
├── Models/              # 4 model classes
├── Services/            # 4 service classes (3 MAIN MODULES)
├── Data/                # 1 data manager class
├── UI/                  # 1 UI class
├── Tests/               # 3 test files
├── Program.cs
└── *.csproj
```

### ✅ 3 Distinct Modules for Video

1. **ClientService** (`Services/ClientService.cs`)
   - Handles all client operations
   - Methods: AddClient, GetAllClients, GetClientById

2. **ProjectService** (`Services/ProjectService.cs`)
   - Handles all project operations
   - Methods: CreateProject, UpdateProjectStatus, AddDeadlineToProject

3. **InvoiceService** (`Services/InvoiceService.cs`)
   - Handles all invoice operations
   - Methods: CreateInvoice, MarkAsPaid, GetTotalRevenue

### ✅ Unit Tests for All 3 Modules

- `ClientServiceTests.cs` - 7 tests
- `ProjectServiceTests.cs` - 8 tests  
- `InvoiceServiceTests.cs` - 10 tests

### ✅ 5+ New Functional Requirements

- FR4: View Upcoming Deadlines
- FR5: Create Invoice
- FR6: View Invoice Status
- FR7: Add New Client
- FR8: View All Clients
- FR9: Update Project Status (BONUS)

---

## Step-by-Step Guide

### STEP 1: Upload Code to GitHub

Replace the old `FreelanceManagementSystem` folder with `FreelanceManagementSystem-v2`:

1. Delete old v1.0.0 code from repository (or rename folder)
2. Upload all files from `FreelanceManagementSystem-v2/`:
   - Models/ folder
   - Services/ folder
   - Data/ folder
   - UI/ folder
   - Tests/ folder
   - Program.cs
   - FreelanceManagementSystem.csproj
   - README.md

---

### STEP 2: Test Locally

```bash
# Navigate to project
cd FreelanceManagementSystem-v2

# Restore packages
dotnet restore

# Build
dotnet build

# Run tests
cd Tests
dotnet test
cd ..

# Run application
dotnet run
```

**Make sure:**
- ✅ All tests pass
- ✅ Application runs without errors
- ✅ All 5 new features work

---

### STEP 3: Update Wiki Documentation

You'll need to update 3 wiki pages with new features.

#### User Documentation

Add sections for:
- FR4: View Upcoming Deadlines
- FR5: Create Invoice
- FR6: View Invoice Status
- FR7: Add New Client
- FR8: View All Clients
- FR9: Update Project Status

#### Development Documentation

Update to include:
- New modular structure
- 3 main service modules
- Unit testing section
- New data models

#### Deployment Documentation

No major changes needed, but update version number to v2.0.0.

---

### STEP 4: Create GitHub Release v2.0.0

#### 4.1 Publish the Application

```bash
dotnet publish -c Release -r win-x64 -o publish/win-x64
```

#### 4.2 Create Archive

```powershell
Compress-Archive -Path publish/win-x64/* -DestinationPath FreelanceManagementSystem-v2.0.0-win-x64.zip
```

#### 4.3 Create Git Tag

```bash
git add .
git commit -m "Release v2.0.0 - Modular architecture with 5 new features and unit tests"
git tag v2.0.0
git push origin main
git push origin v2.0.0
```

#### 4.4 Create Release on GitHub

Go to Releases → Create new release

**Tag:** `v2.0.0`

**Title:** `Version 2.0.0 - Modular Architecture & Enhanced Features`

**Description:**

```markdown
# Freelance Management System v2.0.0

Major update with modular architecture, comprehensive testing, and new features.

## New Features (5+ FR)

✅ **FR4: View Upcoming Deadlines** - See all deadlines sorted by date with overdue highlighting  
✅ **FR5: Create Invoice** - Generate invoices for projects with auto-numbering  
✅ **FR6: View Invoice Status** - Track paid/unpaid invoices with financial summaries  
✅ **FR7: Add New Client** - Full client management system  
✅ **FR8: View All Clients** - View and manage client database  
✅ **FR9: Update Project Status** - Change status to In Progress, Completed, On Hold, Cancelled  

## Technical Improvements

### Modular Architecture
- **Models** - Clean data structures
- **Services** - Business logic separation (3 main modules)
- **Data** - Centralized data management
- **UI** - Separate presentation layer

### Unit Testing
- **25 unit tests** covering all 3 main modules
- **xUnit framework**
- Full test coverage for ClientService, ProjectService, InvoiceService

### Code Quality
- Clear separation of concerns
- Testable design
- SOLID principles
- Comprehensive error handling

## 3 Main Modules

1. **ClientService** - Client management
2. **ProjectService** - Project management  
3. **InvoiceService** - Invoice management

Each module has:
- Dedicated service class
- Comprehensive unit tests
- Clear responsibilities

## Requirements

- .NET 10.0 Runtime or SDK

## Installation

1. Download `FreelanceManagementSystem-v2.0.0-win-x64.zip`
2. Extract files
3. Run `dotnet FreelanceManagementSystem.dll`

## Running Tests

```bash
cd Tests
dotnet test
```

All 25 tests should pass.

## Documentation

- [User Documentation](https://github.com/lazZzybones/CS690-FinalProject/wiki/User-Documentation)
- [Development Documentation](https://github.com/lazZzybones/CS690-FinalProject/wiki/Development-Documentation)
- [Deployment Documentation](https://github.com/lazZzybones/CS690-FinalProject/wiki/Deployment-Documentation)

## What's Working

From v1.0.0:
- Create and manage projects
- Track deadlines
- Persistent storage

New in v2.0.0:
- Client management system
- Invoice tracking and payment status
- View all upcoming deadlines
- Update project statuses
- Modular, testable code structure
- Comprehensive unit tests

This version provides a complete freelance management solution with professional code structure.
```

Upload: `FreelanceManagementSystem-v2.0.0-win-x64.zip`

---

### STEP 5: Prepare Loom Video

#### Show These Items (Rubric):

**1. Show 3 Distinct Modules (12 pts total - 4 pts each)**

Open each file and explain:

1. **ClientService.cs** (Services/ClientService.cs)
   - "This module handles all client operations"
   - Show: AddClient, GetAllClients, GetClientById methods

2. **ProjectService.cs** (Services/ProjectService.cs)
   - "This module handles all project operations"
   - Show: CreateProject, UpdateProjectStatus, AddDeadlineToProject methods

3. **InvoiceService.cs** (Services/InvoiceService.cs)
   - "This module handles all invoice operations"
   - Show: CreateInvoice, GetTotalRevenue, MarkAsPaid methods

**2. Show Tests for 3 Modules (9 pts total - 3 pts each)**

Open each test file:

1. **ClientServiceTests.cs** - Show tests like AddClient_ValidData_ShouldCreateClient
2. **ProjectServiceTests.cs** - Show tests like CreateProject_ValidData_ShouldCreateProject
3. **InvoiceServiceTests.cs** - Show tests like CreateInvoice_ValidData_ShouldCreateInvoice

**3. Run Tests (10 pts)**

```bash
cd Tests
dotnet test
```

Show that all tests pass (5 pts for running + 5 pts for passing)

**4. Run Application (1 pt)**

```bash
dotnet run
```

**5. Show 5 New FR (35 pts total - 7 pts each FR)**

For EACH of the 5 features:
- Show GitHub Issue (2 pts)
- Demonstrate in running app (3 pts)
- Show in User Documentation (2 pts)

Example for FR4:
1. Open GitHub Issues → FR4
2. Run app → Select option 4 (View Upcoming Deadlines)
3. Show working feature
4. Open Wiki → User Documentation → Point to FR4 section

Repeat for FR5, FR6, FR7, FR8.

---

## Rubric Points Breakdown

| Item | Points |
|------|--------|
| Show 3 modules | 12 |
| Show 3 module tests | 9 |
| Run dotnet test | 5 |
| Tests pass | 5 |
| Run application | 1 |
| FR4 (issue, app, docs) | 7 |
| FR5 (issue, app, docs) | 7 |
| FR6 (issue, app, docs) | 7 |
| FR7 (issue, app, docs) | 7 |
| FR8 (issue, app, docs) | 7 |
| **TOTAL** | **67** |

---

## Checklist

### Code ✓
- [ ] All modular code uploaded to GitHub
- [ ] 3 distinct service modules
- [ ] Unit tests for all 3 modules
- [ ] All 5 new FR implemented

### Tests ✓
- [ ] Test project created
- [ ] All tests pass locally
- [ ] Tests cover all 3 modules

### Documentation ✓
- [ ] User Documentation updated with 5 new FR
- [ ] Development Documentation shows modular structure
- [ ] Deployment Documentation updated

### Release ✓
- [ ] v2.0.0 tag created
- [ ] Release published on GitHub
- [ ] Compiled binary attached

### Video Ready ✓
- [ ] Can show 3 modules clearly
- [ ] Can run tests and show passing
- [ ] Can demonstrate all 5 FR
- [ ] GitHub issues for all 5 FR exist

---

## Tips for Success

1. **Module Clarity:** Make sure each service class is clearly a separate module
2. **Test Everything:** Run `dotnet test` multiple times to ensure stability
3. **Video Flow:** Practice showing modules → tests → running app → features
4. **Be Specific:** Point to exact methods and lines when showing modules
5. **Show Passing Tests:** Very important for rubric points!

---

## What Makes v2.0.0 Special

- ✅ **Modular** - Clear separation of concerns
- ✅ **Tested** - 25 unit tests
- ✅ **Professional** - Industry-standard architecture
- ✅ **Feature-Rich** - 9 total features (3 from v1 + 6 from v2)
- ✅ **Maintainable** - Easy to extend and modify

Good luck with Iteration 2! 🚀
