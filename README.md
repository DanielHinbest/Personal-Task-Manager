# Personal Task Manager

A desktop productivity application built with WPF and C#, featuring task management, categorization, priority tracking, and modern MVVM architecture with optional Docker deployment capabilities.

## 🚀 Live Demo

*Coming Soon - Desktop application screenshots and video walkthrough*

## 📋 Project Overview

This desktop application provides a clean, efficient way to manage personal tasks and productivity. Built as a learning project to demonstrate C#/.NET desktop development skills, MVVM architecture, Entity Framework Core, and modern WPF practices.

**Key Achievements:**
- Modern WPF application with MVVM pattern
- SQLite database with Entity Framework Core
- Clean Architecture with separation of concerns
- Responsive desktop UI with data binding
- Docker-compatible console version for deployment scenarios

## ✨ Features

### Core Functionality
- **Task Management**: Create, edit, and delete tasks with detailed descriptions
- **Priority System**: High, Medium, Low priority levels with visual indicators
- **Category Organization**: Custom categories for better task organization
- **Due Date Tracking**: Set and track due dates with overdue indicators
- **Status Management**: Track task completion status
- **Data Persistence**: Reliable SQLite database with Entity Framework Core

### User Experience
- **Modern Interface**: Clean WPF design with intuitive navigation
- **Real-time Updates**: Immediate UI updates with proper data binding
- **Search & Filter**: Find tasks quickly by title, category, or priority
- **Visual Indicators**: Color-coded priorities and status indicators
- **Keyboard Shortcuts**: Common shortcuts for power users
- **Data Validation**: Input validation with user-friendly error messages

### Advanced Features
- **Task Sorting**: Multiple sorting options (date, priority, category)
- **Bulk Operations**: Select and modify multiple tasks at once
- **Data Export**: Export tasks to CSV for external use
- **Backup & Restore**: Database backup and restore functionality
- **Statistics Dashboard**: Overview of task completion and productivity metrics

## 🛠️ Technology Stack

| Component | Technology | Purpose |
|-----------|------------|---------|
| **Desktop Framework** | WPF (.NET 6) | User interface and desktop application framework |
| **Architecture** | MVVM Pattern | Clean separation of concerns and testability |
| **Database** | SQLite + Entity Framework Core | Local data persistence and ORM |
| **MVVM Framework** | CommunityToolkit.Mvvm | MVVM helpers and commands |
| **UI Styling** | Material Design WPF | Modern, consistent UI components |
| **Containerization** | Simple Desktop Deployment | Straightforward Windows executable |
| **Testing** | xUnit + Moq | Unit testing framework and mocking |

## 🏗️ Architecture

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   WPF Views     │◄──►│   ViewModels    │◄──►│   Services      │
│   (UI Layer)    │    │   (MVVM)        │    │ (Business Logic)│
└─────────────────┘    └─────────────────┘    └─────────────────┘
        │                       │                       │
        │              ┌─────────────────┐              │
        └─────────────►│   Data Models   │◄─────────────┘
                       │   (Core Entities)│
                       └─────────────────┘
                                │
                       ┌─────────────────┐
                       │ Entity Framework│
                       │   SQLite DB     │
                       └─────────────────┘
```

### Project Structure
```
PersonalTaskManager/
├── TaskManager.Core/              # Business logic and domain models
│   ├── Models/                    # Task, Category, Priority entities
│   ├── Services/                  # Business logic services
│   └── Interfaces/                # Service contracts
├── TaskManager.Data/              # Data access layer
│   ├── Context/                   # Entity Framework DbContext
│   ├── Repositories/              # Data access patterns
│   └── Migrations/                # Database migrations
├── TaskManager.WPF/               # WPF desktop application
│   ├── Views/                     # WPF windows and user controls
│   ├── ViewModels/                # MVVM view models
│   ├── Converters/                # Value converters for data binding
│   └── Resources/                 # Styles, templates, and resources
└── TaskManager.Tests/             # Unit tests
```

## 🚀 Quick Start

### Prerequisites
- .NET 6.0 SDK or later
- Visual Studio 2022 (recommended) or VS Code
- Git

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

3. **Build the application**
   ```bash
   dotnet build
   ```

4. **Run the WPF application**
   ```bash
   cd TaskManager.WPF
   dotnet run
   ```

### Database Setup
The application automatically creates and initializes the SQLite database on first run:
- Database file: `tasks.db` (created in application directory)
- Automatic migrations applied on startup
- Sample data seeded for demonstration

### Simple Deployment

```bash
# Build the application
dotnet build -c Release

# Create self-contained executable for distribution
dotnet publish -c Release -r win-x64 --self-contained -o ./publish
```

## 📊 Database Schema

SQLite database with Entity Framework Core:

```sql
-- Categories table
CREATE TABLE Categories (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Color TEXT,
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
    CompletedAt DATETIME,
    FOREIGN KEY (CategoryId) REFERENCES Categories (Id)
);
```

## 🔧 Key Implementation Details

### MVVM Pattern Implementation
- **Models**: Pure data entities with validation attributes
- **ViewModels**: Handle UI logic, commands, and data binding using CommunityToolkit.Mvvm
- **Views**: Pure XAML with minimal code-behind
- **Commands**: RelayCommands for user interactions
- **Services**: Dependency injection for business logic

### WPF Modern Practices
- **Data Binding**: Two-way binding with INotifyPropertyChanged
- **Value Converters**: Custom converters for UI data transformation
- **Dependency Injection**: Built-in .NET DI container
- **Resource Management**: Centralized styles and templates
- **Responsive Design**: Dynamic layouts that adapt to window size

### Entity Framework Core Integration
- **Code-First Approach**: Models define database schema
- **Migrations**: Version-controlled database changes
- **Repository Pattern**: Abstracted data access
- **Async Operations**: Non-blocking database operations
- **Connection Management**: Proper disposal and connection pooling

## 📈 Development Progress & Future Enhancements

### ✅ Phase 1: Core Foundation (PLANNED)
**Goal**: Basic task management with modern architecture

**Planned Features:**
- ✅ Project setup with proper architecture
- ✅ Basic MVVM implementation with CommunityToolkit
- ✅ SQLite database with Entity Framework Core
- ✅ Core task CRUD operations
- ✅ Basic WPF UI with data binding
- ✅ Category management system
- ✅ Priority and status tracking

---

### Phase 2: Enhanced User Experience
**Goal**: Polished UI and advanced features

**Planned Features:**
- Modern UI styling with Material Design WPF
- Advanced search and filtering capabilities
- Task sorting and grouping options
- Keyboard shortcuts and accessibility features
- Data export functionality (CSV, PDF)
- Statistics and productivity insights
- Custom themes and UI customization

**Technical Implementation:**
- Implement Material Design styling
- Add complex filtering with LINQ expressions
- Create custom user controls for repeated UI elements
- Implement keyboard navigation and shortcuts
- Add PDF generation with iTextSharp
- Create charts for productivity visualization
- Implement theme switching with resource dictionaries

**Learning Objectives:**
- Advanced WPF styling and theming
- Complex data binding scenarios
- Custom control development
- File I/O and data export formats
- Accessibility best practices

---

### Phase 3: Advanced Features & Performance
**Goal**: Production-ready application with enterprise features

**Planned Features:**
- Multi-user support with user profiles
- Task templates and recurring tasks
- File attachments and notes
- Reminders and notifications
- Synchronization with cloud services
- Performance optimizations for large datasets
- Comprehensive unit test coverage

**Technical Implementation:**
- Add user authentication system
- Implement background services for reminders
- Create plugin architecture for extensibility
- Add cloud sync with REST APIs
- Optimize database queries and indexing
- Implement caching strategies
- Add comprehensive logging and error handling

**Deployment & Distribution:**
- Create MSI installer with WiX Toolset
- Set up automated builds with GitHub Actions
- Implement auto-update functionality
- Create comprehensive documentation
- Add telemetry and crash reporting

**Learning Objectives:**
- Desktop application deployment
- Background services in WPF
- Cloud integration patterns
- Performance optimization techniques
- Application lifecycle management

---

### Phase 4: Enterprise & Integration Features
**Goal**: Professional-grade productivity tool

**Planned Features:**
- Team collaboration features
- Integration with popular tools (Outlook, Teams)
- Advanced reporting and analytics
- API for third-party integrations
- Mobile companion app planning
- Enterprise deployment options

**Technical Implementation:**
- Build REST API for external integrations
- Implement real-time collaboration features
- Create advanced reporting engine
- Add Office 365 integration
- Design mobile-friendly data synchronization
- Implement enterprise security features

**Learning Objectives:**
- API design and documentation
- Real-time communication protocols
- Enterprise integration patterns
- Cross-platform data synchronization
- Security and compliance considerations

## 🌟 Portfolio Highlights

This project demonstrates:

### Desktop Development Skills
- **Modern WPF**: Current best practices with .NET 6 and modern XAML
- **MVVM Mastery**: Clean architecture with proper separation of concerns
- **Data Binding**: Complex scenarios with converters and validation
- **Entity Framework**: Code-first approach with migrations and relationships
- **Dependency Injection**: Proper IoC container usage in desktop applications

### Software Architecture
- **Clean Architecture**: Well-organized project structure with clear boundaries
- **Design Patterns**: MVVM, Repository, and Command patterns
- **SOLID Principles**: Maintainable and testable code structure
- **Async Programming**: Proper async/await patterns for UI responsiveness
- **Error Handling**: Comprehensive exception handling and user feedback

### Modern Development Practices
- **Version Control**: Git workflow with meaningful commits
- **Testing**: Unit tests with mocking and dependency injection
- **Documentation**: Comprehensive README and code documentation
- **Containerization**: Docker support for alternative deployment scenarios
- **CI/CD Ready**: Structured for automated build and deployment

## 🧪 Testing

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test TaskManager.Tests
```

## 📦 Deployment

### Desktop Distribution
```bash
# Build for development
dotnet build

# Create release build
dotnet build -c Release

# Create self-contained executable for distribution
dotnet publish -c Release -r win-x64 --self-contained -o ./publish
```

## 🤝 Contributing

Contributions are welcome! Please feel free to:
- Report bugs or request features
- Submit pull requests for improvements
- Provide feedback on user experience
- Contribute to documentation and examples

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

---

**Built with 🎯 as a learning project to demonstrate modern C# desktop development, MVVM architecture, and Entity Framework Core skills.**