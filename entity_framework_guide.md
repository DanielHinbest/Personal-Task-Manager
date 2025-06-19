# WPF + SQLite + Entity Framework Setup Guide

## 1. Create New WPF Project

```bash
dotnet new wpf -n MyWpfApp
cd MyWpfApp
```

## 2. Install Required NuGet Packages

```bash
# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore

# SQLite provider for EF Core
dotnet add package Microsoft.EntityFrameworkCore.Sqlite

# EF Core Tools (for migrations)
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Design-time tools
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## 3. Create Your Model Classes

Create a `Models` folder and add your entity classes:

```csharp
// Models/User.cs
namespace MyWpfApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
```

## 4. Create DbContext

Create a `Data` folder and add your DbContext:

```csharp
// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using MyWpfApp.Models;

namespace MyWpfApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite connection string - database file will be created in the app directory
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here if needed
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            });
        }
    }
}
```

## 5. Initialize Database in App.xaml.cs

```csharp
// App.xaml.cs
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MyWpfApp.Data;

namespace MyWpfApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            // Ensure database is created
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
```

## 6. Create a Simple Service Class

```csharp
// Services/UserService.cs
using Microsoft.EntityFrameworkCore;
using MyWpfApp.Data;
using MyWpfApp.Models;

namespace MyWpfApp.Services
{
    public class UserService
    {
        public async Task<List<User>> GetAllUsersAsync()
        {
            using var context = new AppDbContext();
            return await context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            using var context = new AppDbContext();
            return await context.Users.FindAsync(id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            using var context = new AppDbContext();
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            using var context = new AppDbContext();
            context.Users.Update(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            using var context = new AppDbContext();
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
```

## 7. Update MainWindow.xaml

```xml
<!-- MainWindow.xaml -->
<Window x:Class="MyWpfApp.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="WPF SQLite EF Demo" Height="450" Width="800">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto"/>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <!-- Input Form -->
        <StackPanel Grid.Row="0" Margin="10" Orientation="Horizontal">
            <TextBox x:Name="NameTextBox" Width="150" Margin="5" PlaceholderText="Name"/>
            <TextBox x:Name="EmailTextBox" Width="200" Margin="5" PlaceholderText="Email"/>
            <Button x:Name="AddButton" Content="Add User" Margin="5" Click="AddButton_Click"/>
            <Button x:Name="RefreshButton" Content="Refresh" Margin="5" Click="RefreshButton_Click"/>
        </StackPanel>

        <!-- Users List -->
        <DataGrid x:Name="UsersDataGrid" Grid.Row="1" Margin="10" AutoGenerateColumns="True"/>

        <!-- Status -->
        <TextBlock x:Name="StatusTextBlock" Grid.Row="2" Margin="10" Text="Ready"/>
    </Grid>
</Window>
```

## 8. Update MainWindow.xaml.cs

```csharp
// MainWindow.xaml.cs
using System.Windows;
using MyWpfApp.Models;
using MyWpfApp.Services;

namespace MyWpfApp
{
    public partial class MainWindow : Window
    {
        private readonly UserService _userService;

        public MainWindow()
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUsers();
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || 
                string.IsNullOrWhiteSpace(EmailTextBox.Text))
            {
                StatusTextBlock.Text = "Please enter both name and email";
                return;
            }

            try
            {
                var user = new User
                {
                    Name = NameTextBox.Text.Trim(),
                    Email = EmailTextBox.Text.Trim()
                };

                await _userService.AddUserAsync(user);
                
                NameTextBox.Clear();
                EmailTextBox.Clear();
                StatusTextBlock.Text = "User added successfully";
                
                await LoadUsers();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error: {ex.Message}";
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                UsersDataGrid.ItemsSource = users;
                StatusTextBlock.Text = $"Loaded {users.Count} users";
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Error loading users: {ex.Message}";
            }
        }
    }
}
```

## 9. Alternative: Using Migrations (Recommended for Production)

Instead of `EnsureCreated()`, use migrations for better database management:

```bash
# Add initial migration
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

## 10. Project Structure

Your project should look like this:

```
MyWpfApp/
├── Data/
│   └── AppDbContext.cs
├── Models/
│   └── User.cs
├── Services/
│   └── UserService.cs
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
└── MyWpfApp.csproj
```

## Key Points

- **SQLite Database**: The database file (`app.db`) will be created in your application's directory
- **Async Operations**: All database operations are async to keep the UI responsive
- **Using Statements**: Properly dispose of DbContext instances
- **Error Handling**: Always wrap database operations in try-catch blocks
- **Connection String**: You can modify the connection string in `OnConfiguring()` to change the database location

## Next Steps

1. Add data validation
2. Implement MVVM pattern with ViewModels
3. Add dependency injection
4. Create more complex entity relationships
5. Add logging
6. Implement proper error handling and user feedback