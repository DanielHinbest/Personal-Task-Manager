# Personal Task Manager

A desktop productivity application built with WPF and C#, featuring task management, categorization, and priority tracking with SQLite database integration.

## 🚀 Live Demo

*Desktop application - screenshots and walkthrough coming soon*

## 📋 Project Overview

This desktop application provides a clean, efficient way to manage personal tasks. Built as a portfolio project to demonstrate C#/.NET desktop development skills, Entity Framework Core, and modern WPF practices.

**Key Achievements:**
- Modern WPF application with clean architecture
- SQLite database with Entity Framework Core
- Service layer for business logic separation
- Responsive desktop UI with proper data binding

## ✨ Features

### Core Functionality
- **Task Management**: Create, edit, and delete tasks with detailed descriptions
- **Priority System**: High, Medium, Low priority levels with visual indicators
- **Category Organization**: Custom categories for better task organization
- **Due Date Tracking**: Set and track due dates
- **Status Management**: Track task completion status
- **Data Persistence**: Reliable SQLite database with Entity Framework Core

### User Experience
- **Clean Interface**: Intuitive WPF design with easy navigation
- **Real-time Updates**: Immediate UI updates when data changes
- **Visual Indicators**: Color-coded priorities and status indicators
- **Data Validation**: Input validation with user-friendly error messages

## 🛠️ Technology Stack

| Component | Technology | Purpose |
|-----------|------------|---------|
| **Desktop Framework** | WPF (.NET 8) | User interface and desktop application framework |
| **Database** | SQLite + Entity Framework Core | Local data persistence and ORM |
| **Architecture** | Service Layer Pattern | Clean separation of UI and business logic |

## 🏗️ Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   WPF Windows   │◄──►│   Services      │◄──►│ Entity Framework│
│   (UI Layer)    │    │ (Business Logic)│    │   SQLite DB     │
└─────────────────┘    └─────────────────┘    └─────────────────┘
        │                       │                       │
        └──────────────────────►│   Data Models   │◄────┘
                                │ (Task, Category)│
                                └─────────────────┘
```

### Project Structure
```
Personal-Task-Manager/
├── Data/
│   └── AppDbContext.cs           # Entity Framework context
├── Models/
│   ├── Task.cs                   # Task entity
│   └── Category.cs               # Category entity
├── Services/
│   ├── TaskService.cs            # Task business logic
│   └── CategoryService.cs        # Category business logic
├── MainWindow.xaml               # Main application window
├── AddEditTaskWindow.xaml        # Add/Edit task dialog
└── App.xaml.cs                   # Application startup
```

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK or later
- Visual Studio 2022 (recommended) or VS Code

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/personal-task-manager.git
   cd personal-task-manager
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build and run**
   ```bash
   dotnet build
   dotnet run
   ```

### Database Setup
The application automatically creates the SQLite database on first run:
- Database file: `tasks.db` (created in application directory)
- Tables created automatically using Entity Framework

## 📊 Database Schema

SQLite database with Entity Framework Core:

```sql
-- Categories table
CREATE TABLE Categories (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Tasks table
CREATE TABLE Tasks (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Description TEXT,
    Priority INTEGER NOT NULL, -- 0=Low, 1=Medium, 2=High
    Status INTEGER NOT NULL,   -- 0=Pending, 1=InProgress, 2=Completed
    DueDate DATETIME,
    CategoryId INTEGER,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryId) REFERENCES Categories (Id)
);
```

## 🔧 Key Implementation Details

### Entity Framework Core Integration
- **Code-First Approach**: Models define database schema
- **SQLite Provider**: Lightweight, file-based database
- **Service Layer**: Clean separation between UI and data access
- **Async Operations**: Non-blocking database operations for responsive UI

### WPF Implementation
- **Data Binding**: Two-way binding for form controls
- **Event Handling**: Click events and form validation
- **Dialog Windows**: Modal dialogs for task creation/editing
- **User Controls**: Reusable UI components

### Simple Architecture Benefits
- **Easy to Understand**: Clear separation without over-engineering
- **Maintainable**: Straightforward code structure
- **Testable**: Service layer can be unit tested
- **Extensible**: Easy to add new features

## 🌟 Portfolio Highlights

This project demonstrates:

### Desktop Development Skills
- **Modern WPF**: Current best practices with .NET 8
- **Entity Framework Core**: Database integration with code-first approach
- **Service Layer Pattern**: Clean architecture without over-complication
- **SQLite Integration**: Lightweight database for desktop applications

### Software Development Fundamentals
- **Clean Code**: Well-organized project structure
- **Separation of Concerns**: UI separated from business logic
- **Data Persistence**: Proper database design and relationships
- **Error Handling**: User-friendly error messages and validation

### Practical Skills
- **Real-world Application**: Solves actual productivity needs
- **User Experience**: Intuitive interface design
- **Data Management**: CRUD operations with proper validation
- **Desktop Deployment**: Self-contained executable creation

## 🧪 Testing

```bash
# Build the application
dotnet build

# Run the application
dotnet run
```

## 📦 Deployment

```bash
# Create release build
dotnet build -c Release

# Create self-contained executable
dotnet publish -c Release -r win-x64 --self-contained -o ./publish
```

The published folder will contain a standalone executable that can run on Windows without requiring .NET to be installed.

## 🚀 Future Enhancements

Potential improvements for learning and portfolio expansion:

- **Enhanced UI**: Material Design styling for modern look
- **Data Export**: Export tasks to CSV format
- **Search & Filter**: Find tasks by keywords or criteria
- **Notifications**: Reminders for due dates
- **Themes**: Light/dark mode support

## 🤝 Contributing

This is a learning/portfolio project, but feedback and suggestions are welcome!

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

**Built as a portfolio project to demonstrate modern C# desktop development with WPF, Entity Framework Core, and clean architecture principles.**