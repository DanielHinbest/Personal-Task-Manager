using Personal_Task_Manager.Models;
using Personal_Task_Manager.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Task = Personal_Task_Manager.Models.Task;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Task> _allTasks = new();
        private ObservableCollection<Task> _filteredTasks = new();
        private string _searchText = string.Empty;
        private string _selectedPriorityFilter = "All";
        private string _selectedStatusFilter = "All";
        private int? _selectedCategoryFilter;

        public ObservableCollection<Task> Tasks
        {
            get => _allTasks;
            set
            {
                _allTasks = value;
                OnPropertyChanged(nameof(Tasks));
                ApplyFilters();
            }
        }

        public ObservableCollection<Task> FilteredTasks
        {
            get => _filteredTasks;
            private set
            {
                _filteredTasks = value;
                OnPropertyChanged(nameof(FilteredTasks));
                OnPropertyChanged(nameof(TasksSummary));
            }
        }

        public ObservableCollection<Category> Categories { get; set; } = new();

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                ApplyFilters();
            }
        }

        public string SelectedPriorityFilter
        {
            get => _selectedPriorityFilter;
            set
            {
                _selectedPriorityFilter = value;
                OnPropertyChanged(nameof(SelectedPriorityFilter));
                ApplyFilters();
            }
        }

        public string SelectedStatusFilter
        {
            get => _selectedStatusFilter;
            set
            {
                _selectedStatusFilter = value;
                OnPropertyChanged(nameof(SelectedStatusFilter));
                ApplyFilters();
            }
        }

        public int? SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                _selectedCategoryFilter = value;
                OnPropertyChanged(nameof(SelectedCategoryFilter));
                ApplyFilters();
            }
        }

        public string TasksSummary
        {
            get
            {
                var total = FilteredTasks.Count;
                var completed = FilteredTasks.Count(t => t.Status == "Completed");
                var pending = FilteredTasks.Count(t => t.Status == "Pending");
                var inProgress = FilteredTasks.Count(t => t.Status == "In Progress");

                return $"Total: {total} | Completed: {completed} | In Progress: {inProgress} | Pending: {pending}";
            }
        }

        public string LastUpdated => DateTime.Now.ToString("MMM dd, yyyy HH:mm");

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadTasksAsync();
            await LoadCategoriesAsync();
            
            // Set default selections after data is loaded
            SelectedPriorityFilter = "All";
            SelectedStatusFilter = "All";
            SelectedCategoryFilter = 0;
        }

        private async void btnAddNewTask_Click(object sender, RoutedEventArgs e)
        {
            AddEditTaskWindow taskWindow = new AddEditTaskWindow();
            bool? result = taskWindow.ShowDialog();
            if (result == true)
            {
                await LoadTasksAsync();
            }
        }

        private async System.Threading.Tasks.Task LoadTasksAsync()
        {
            try
            {
                var service = new Services.TaskService();
                var tasks = await service.GetAllTasksAsync();

                Tasks.Clear();
                foreach (var task in tasks)
                {
                    Tasks.Add(task);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async System.Threading.Tasks.Task LoadCategoriesAsync()
        {
            try
            {
                var service = new Services.CategoryService();
                var categories = await service.GetAllCategoriesAsync();

                Categories.Clear();
                Categories.Add(new Category { Id = 0, Name = "All Categories", CreatedAt = DateTime.Now, Tasks = new List<Task>() });
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
                OnPropertyChanged(nameof(Categories));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ApplyFilters()
        {
            if (Tasks == null || !Tasks.Any())
            {
                FilteredTasks = new ObservableCollection<Task>();
                return;
            }

            var filtered = Tasks.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var searchLower = SearchText.ToLower();
                filtered = filtered.Where(t => 
                    t.Title.ToLower().Contains(searchLower) || 
                    t.Description.ToLower().Contains(searchLower) ||
                    (t.Category?.Name?.ToLower().Contains(searchLower) ?? false));
            }

            // Apply priority filter
            if (!string.IsNullOrEmpty(SelectedPriorityFilter) && SelectedPriorityFilter != "All")
            {
                filtered = filtered.Where(t => t.Priority == SelectedPriorityFilter);
            }

            // Apply status filter
            if (!string.IsNullOrEmpty(SelectedStatusFilter) && SelectedStatusFilter != "All")
            {
                var statusToMatch = SelectedStatusFilter == "InProgress" ? "In Progress" : SelectedStatusFilter;
                filtered = filtered.Where(t => t.Status == statusToMatch);
            }

            // Apply category filter
            if (SelectedCategoryFilter.HasValue && SelectedCategoryFilter.Value > 0)
            {
                filtered = filtered.Where(t => t.CategoryId == SelectedCategoryFilter.Value);
            }

            FilteredTasks = new ObservableCollection<Task>(filtered.ToList());
        }

        private async void btnCompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Task task)
            {
                task.Status = "Completed";
                task.UpdatedAt = DateTime.Now;

                TaskService taskService = new TaskService();
                await taskService.UpdateTaskAsync(task);

                await LoadTasksAsync();
            }
        }

        private async void btnDeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Task task)
            {
                var result = MessageBox.Show(
                    $"Are you sure you want to delete the task '{task.Title}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    TaskService taskService = new TaskService();
                    await taskService.DeleteTaskAsync(task.Id);
                    await LoadTasksAsync();
                }
            }
        }

        private async void btnEditTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Task task)
            {
                // Fetch the latest task from the database
                TaskService service = new TaskService();
                var dbTask = await service.GetTaskByIdAsync(task.Id);

                if (dbTask != null)
                {
                    AddEditTaskWindow editWindow = new AddEditTaskWindow(dbTask);
                    bool? result = editWindow.ShowDialog();
                    if (result == true)
                    {
                        await LoadTasksAsync();
                    }
                }
                else
                {
                    MessageBox.Show("Task not found in the database.");
                }
            }
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnRefresh.IsEnabled = false;
                await LoadTasksAsync();
                await LoadCategoriesAsync();
                OnPropertyChanged(nameof(LastUpdated));
            }
            finally
            {
                btnRefresh.IsEnabled = true;
            }
        }

        private async void btnManageCategories_Click(object sender, RoutedEventArgs e)
        {
            ManageCategoriesWindow categoriesWindow = new ManageCategoriesWindow();
            categoriesWindow.Owner = this;
            bool? result = categoriesWindow.ShowDialog();
            
            // Refresh categories after the dialog closes
            await LoadCategoriesAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}