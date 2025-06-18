# Film Zone 🎬

A comprehensive movie management system built with ASP.NET Core MVC, featuring role-based access control and advanced file management over movies ,categories and Streaming Services
**🔗 Live Repository**: [https://github.com/WesamMagdy/TheFilmZone](https://github.com/WesamMagdy/TheFilmZone)

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Architecture](#architecture)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Configuration](#configuration)
- [Usage](#usage)
- [Project Structure](#project-structure)

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
- **Advanced Search Functionality**: Search movies by name with real-time filtering
- **Category-Based Filtering**: Browse movies by categories
- **Smart File Management**: 
  - Unique filename generation using GUIDs
  - Automatic old file cleanup during updates
  - Secure file deletion when movies are removed
- **Movie Cover Upload**: Secure image upload with comprehensive validation
- **Smart Validation**: Prevent deletion of categories that contain movies
- **Name Validation**: Ensure unique and valid movie/category names
- **Data Integrity**: Comprehensive server-side and client-side validation

### 🛡️ Security & Validation
- **File Upload Security**: 
  - Extension validation (images only)
  - File size limits and validation
  - Secure server-side storage with GUID-based naming
  - Automatic cleanup of old files during updates
- **Business Logic Validation**:
  - Prevent deletion of non-empty categories
  - Unique name enforcement: No duplicate movie names, category names, or role names
  - Required field validation across all forms
  - Referential integrity checks
- **Role-Based Authorization**: 
  - Granular access control for CRUD operations
  - Anonymous access for movie details viewing
  - Admin-only access for management functions
- **Anti-Forgery Protection**: CSRF protection on all state-changing operations

## 🏗️ Architecture

Film Zone follows a clean, layered architecture pattern:

```
┌─────────────────────┐
│   Presentation      │  ← MVC Controllers & Views
│   (Web Layer)       │
├─────────────────────┤
│   Provider Layer    │  ← ViewModel ↔ Model Mapping
├─────────────────────┤
│   Service Layer     │  ← Business Logic
├─────────────────────┤
│   Infrastructure    │  ← Data Access & Repository
│   Layer             │
└─────────────────────┘
```

### Key Architectural Patterns
- **MVC Pattern**: Clear separation of concerns
- **Provider Pattern**: Controllers interact with providers for data transformation
- **Service Layer**: Business logic isolated from presentation concerns
- **Repository Pattern**: Generic repository implementation for data access
- **Dependency Injection**: Loose coupling and testability throughout all layers

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
    "RoleNameMaxLength": 50,
    "EnforceUniqueNames": {
      "Movies": true,
      "Categories": true,
      "Roles": true
    },
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
   - Edit existing movies and update their details (with smart file replacement)
   - Delete movies with automatic file cleanup
   - Search and filter movies by name
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
2. **Browse Movies**: View available movies and their details (no login required)
3. **Search Functionality**: Find movies by name using the search feature
4. **Filter by Category**: Find movies organized by categories
5. **View Movie Details**: See movie information including cover images and streaming options

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

**Film Zone** - Built with ❤️ using ASP.NET Core
