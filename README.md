# Film Zone 🎬

A comprehensive movie management system built with ASP.NET Core MVC, featuring role-based access control over movies,categories and streaming services

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [API Endpoints](#api-endpoints)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

Film Zone is a modern web application designed for managing movies and categories with comprehensive user management. Built with a clean architecture approach, it provides a robust platform for administrators to manage film databases while offering users an intuitive interface to browse and discover movies.

## ✨ Features

### 🎭 User Management & Authentication
- **User Registration & Login**: Secure account creation and authentication
- **Role-Based Access Control**: Different permission levels for users and administrators
- **Account Management**: Admin panel for managing user accounts and role assignments
- **Dynamic Role Management**: Create, edit, and delete custom roles with specific permissions

### 🎬 Movie & Category Management
- **Movie CRUD Operations**: Complete movie lifecycle management (Admin only)
- **Category Management**: Create, edit, and delete movie categories (Admin only)
- **Movie Cover Upload**: Secure image upload with file validation
- **Smart Validation**: Prevent deletion of categories that contain movies
- **Name Validation**: Ensure unique and valid movie/category names
- **Data Integrity**: Comprehensive server-side and client-side validation

### 🛡️ Security & Validation
- **File Upload Security**: 
  - Extension validation (images only)
  - File size limits and validation
  - Secure server-side storage
- **Business Logic Validation**:
  - Prevent deletion of non-empty categories
  - Duplicate name prevention for movies and categories
  - Required field validation across all forms
- **Role-Based Authorization**: Granular access control for CRUD operations

## 🏗️ Architecture

Film Zone follows a clean, layered architecture pattern:

```
┌─────────────────────┐
│   Presentation      │  ← MVC Controllers & Views
│   (Web Layer)       │
├─────────────────────┤
│   Service Layer     │  ← Business Logic
├─────────────────────┤
│   Provider Layer    │  ← ViewModel ↔ Model Mapping
├─────────────────────┤
│   Infrastructure    │  ← Data Access & Repository
│   Layer             │
└─────────────────────┘
```

### Key Architectural Patterns
- **MVC Pattern**: Clear separation of concerns
- **Repository Pattern**: Generic repository implementation for data access
- **Dependency Injection**: Loose coupling and testability
- **Service Layer**: Centralized business logic
- **Provider Pattern**: Clean data transformation between layers

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core MVC
- **Authentication**: ASP.NET Core Identity
- **Database**: Entity Framework Core
- **Architecture**: Clean Architecture with DI Container
- **Validation**: Data Annotations + Custom Validators
- **File Storage**: Server-side file management
- **Frontend**: Razor Views with client-side validation

## 📋 Prerequisites

- .NET 6.0 or later
- SQL Server (LocalDB or Full Instance)
- Visual Studio 2022 or VS Code
- Git

## 🚀 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/film-zone.git
   cd film-zone
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Update database connection string**
   - Open `appsettings.json`
   - Modify the connection string to match your SQL Server setup

4. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Navigate to `https://localhost:5001` or `http://localhost:5000`

## ⚙️ Configuration

### Database Configuration
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FilmZoneDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### File Upload Settings
Configure file upload validation in `appsettings.json`:
```json
{
  "FileUpload": {
    "MaxFileSize": 5242880,
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".gif"],
    "UploadPath": "wwwroot/uploads/covers"
  }
}
```

### Validation Settings
Configure business rules and validation:
```json
{
  "Validation": {
    "MovieNameMaxLength": 100,
    "CategoryNameMaxLength": 50,
    "RequireUniqueNames": true,
    "PreventNonEmptyCategoryDeletion": true
  }
}
```

### Default Admin Account
The system creates a default admin account on first run:
- **Username**: admin@filmzone.com
- **Password**: Admin123!
- **Role**: Administrator

## 💡 Usage

### For Administrators
1. **Login** with admin credentials
2. **Movie Management**: 
   - Create new movies with cover image upload
   - Edit existing movies and update their details
   - Delete movies (with proper validation)
   - View all movies in the system
3. **Category Management**: 
   - Create new categories for organizing movies
   - Edit existing categories
   - Delete categories (only if they contain no movies)
   - Manage category-movie relationships
4. **User & Role Management**: 
   - View all registered users
   - Assign and remove roles from users
   - Create, edit, and delete custom roles
   - Manage role-based permissions

### For Regular Users
1. **Account Management**: Register for a new account or login
2. **Browse Movies**: View available movies and their details
3. **Filter by Category**: Find movies organized by categories
4. **View Movie Details**: See movie information including cover images

## 📁 Project Structure

```
FilmZone/
├── Controllers/           # MVC Controllers
├── Views/                # Razor Views
├── Models/               # Data Models
├── ViewModels/           # Presentation Models
├── Services/             # Business Logic Layer
├── Providers/            # Data Transformation Layer
├── Infrastructure/       # Data Access Layer
│   ├── Repositories/     # Repository Implementations
│   └── Data/            # DbContext & Configurations
├── wwwroot/             # Static Files
│   └── uploads/         # File Upload Directory
└── Areas/               # Identity Areas
    └── Identity/        # Authentication Views
```

## 🔌 API Endpoints

### Movies
- `GET /Movies` - List all movies
- `GET /Movies/Details/{id}` - Get movie details
- `POST /Movies/Create` - Create new movie with cover upload (Admin)
- `PUT /Movies/Edit/{id}` - Update movie and cover (Admin)
- `DELETE /Movies/Delete/{id}` - Delete movie (Admin)

### Categories
- `GET /Categories` - List all categories
- `GET /Categories/Details/{id}` - Get category with movies
- `POST /Categories/Create` - Create category (Admin)
- `PUT /Categories/Edit/{id}` - Update category (Admin)
- `DELETE /Categories/Delete/{id}` - Delete category if empty (Admin)

### User Management
- `GET /Users` - List all users (Admin)
- `GET /Users/Details/{id}` - Get user details (Admin)
- `POST /Users/AssignRole` - Assign role to user (Admin)
- `POST /Users/RemoveRole` - Remove role from user (Admin)

### Role Management
- `GET /Roles` - List all roles (Admin)
- `POST /Roles/Create` - Create new role (Admin)
- `PUT /Roles/Edit/{id}` - Update role (Admin)
- `DELETE /Roles/Delete/{id}` - Delete role (Admin)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Development Guidelines
- Follow clean architecture principles with proper layer separation
- Maintain async/await patterns for all database operations
- Implement comprehensive validation (server-side and client-side):
  - File upload validation (size, extension, security)
  - Business rule validation (unique names, referential integrity)
  - Prevent deletion of categories containing movies
- Use dependency injection for all services and repositories
- Include unit tests for business logic and validation rules
- Update documentation for significant changes

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

If you encounter any issues or have questions:
- Create an issue in the GitHub repository
- Check the [Wiki](../../wiki) for additional documentation
- Review existing issues for similar problems

---

**Film Zone** - Built with ❤️ using ASP.NET Core
