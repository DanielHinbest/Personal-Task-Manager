using Personal_Task_Manager.Models;
using Personal_Task_Manager.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Task = System.Threading.Tasks.Task;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for AddEditTaskWindow.xaml
    /// </summary>
    public partial class AddEditTaskWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Category> Categories { get; set; } = new();
        private int? _pendingCategoryId;

        private string _taskTitle = string.Empty;
        private string _taskDescription = string.Empty;
        private string _selectedPriority = "Medium";
        private string _selectedStatus = "Pending";
        private DateTime? _dueDate;
        private int? _selectedCategoryId;

        public string TaskTitle
        {
            get => _taskTitle;
            set
            {
                _taskTitle = value;
                OnPropertyChanged(nameof(TaskTitle));
            }
        }

        public string TaskDescription
        {
            get => _taskDescription;
            set
            {
                _taskDescription = value;
                OnPropertyChanged(nameof(TaskDescription));
            }
        }

        public string SelectedPriority
        {
            get => _selectedPriority;
            set
            {
                _selectedPriority = value;
                OnPropertyChanged(nameof(SelectedPriority));
            }
        }

        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                OnPropertyChanged(nameof(SelectedStatus));
            }
        }

        public DateTime? DueDate
        {
            get => _dueDate;
            set
            {
                _dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        public int? SelectedCategoryId
        {
            get => _selectedCategoryId;
            set
            {
                _selectedCategoryId = value;
                OnPropertyChanged(nameof(SelectedCategoryId));
            }
        }

        public string WindowTitle => IsEditMode ? "Edit Task" : "Add New Task";
        public string WindowSubtitle => IsEditMode ? "Update task details" : "Create a new task";

        public AddEditTaskWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += AddEditTaskWindow_Loaded;
        }

        public AddEditTaskWindow(Personal_Task_Manager.Models.Task taskToEdit)
            : this()
        {
            TaskTitle = taskToEdit.Title;
            TaskDescription = taskToEdit.Description;
            SelectedPriority = taskToEdit.Priority;
            SelectedStatus = taskToEdit.Status;
            DueDate = taskToEdit.DueDate;
            SelectedCategoryId = taskToEdit.CategoryId;

            IsEditMode = true;
            EditingTaskId = taskToEdit.Id;
            _originalCreatedAt = taskToEdit.CreatedAt;
            _pendingCategoryId = taskToEdit.CategoryId;
            
            // Notify that window title properties have changed
            OnPropertyChanged(nameof(WindowTitle));
            OnPropertyChanged(nameof(WindowSubtitle));
        }

        public bool IsEditMode { get; set; } = false;
        public int EditingTaskId { get; set; } = 0;
        private DateTime _originalCreatedAt;

        private async void AddEditTaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategoriesAsync();
            
            if (_pendingCategoryId.HasValue)
            {
                SelectedCategoryId = _pendingCategoryId;
                _pendingCategoryId = null;
            }

            // Set ComboBox values after everything is loaded
            SetComboBoxValues();
        }

        private void SetComboBoxValues()
        {
            // Set priority ComboBox
            bool priorityFound = false;
            foreach (ComboBoxItem item in cmbPriority.Items)
            {
                if (item.Tag?.ToString() == SelectedPriority)
                {
                    cmbPriority.SelectedItem = item;
                    priorityFound = true;
                    break;
                }
            }
            
            // If priority not found, default to Medium
            if (!priorityFound)
            {
                foreach (ComboBoxItem item in cmbPriority.Items)
                {
                    if (item.Tag?.ToString() == "Medium")
                    {
                        cmbPriority.SelectedItem = item;
                        break;
                    }
                }
            }

            // Set status ComboBox - handle "In Progress" vs "InProgress" discrepancy
            bool statusFound = false;
            string statusToMatch = SelectedStatus;
            
            // Convert "In Progress" to "InProgress" for matching
            if (statusToMatch == "In Progress")
                statusToMatch = "InProgress";

            foreach (ComboBoxItem item in cmbStatus.Items)
            {
                if (item.Tag?.ToString() == statusToMatch)
                {
                    cmbStatus.SelectedItem = item;
                    statusFound = true;
                    break;
                }
            }
            
            // If status not found, default to Pending
            if (!statusFound)
            {
                foreach (ComboBoxItem item in cmbStatus.Items)
                {
                    if (item.Tag?.ToString() == "Pending")
                    {
                        cmbStatus.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        private async void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            NewCategoryDialog newCategory = new NewCategoryDialog();
            bool? result = newCategory.ShowDialog();
            if (result == true)
            {
                await LoadCategoriesAsync();
            }
        }

        private async System.Threading.Tasks.Task LoadCategoriesAsync()
        {
            var service = new Services.CategoryService();
            var categories = await service.GetAllCategoriesAsync();

            Categories.Clear();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
            OnPropertyChanged(nameof(Categories));
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CategoryService categoryService = new CategoryService();
            
            // Get values from ComboBoxes
            string title = TaskTitle;
            string description = TaskDescription;
            string priority = (cmbPriority.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Medium";
            string status = (cmbStatus.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Pending";
            
            // Convert "InProgress" back to "In Progress" for storage
            if (status == "InProgress")
                status = "In Progress";
                
            DateTime? dueDate = DueDate;

            if (string.IsNullOrWhiteSpace(title) || dueDate == null)
            {
                MessageBox.Show("Please enter a title and select a due date.");
                return;
            }

            var category = await categoryService.GetCategoryByIdAsync(SelectedCategoryId ?? 0);
            if (category == null)
            {
                MessageBox.Show("Please select a valid category.");
                return;
            }

            TaskService taskService = new TaskService();

            if (IsEditMode)
            {
                // Update existing task
                var updatedTask = new Personal_Task_Manager.Models.Task
                {
                    Id = EditingTaskId,
                    Title = title,
                    Description = description,
                    Priority = priority,
                    Status = status,
                    DueDate = dueDate.Value,
                    CategoryId = category.Id,
                    Category = category,
                    UpdatedAt = DateTime.Now,
                    CreatedAt = _originalCreatedAt
                };
                await taskService.UpdateTaskAsync(updatedTask);
            }
            else
            {
                // Add new task
                await taskService.AddTaskAsync(title, description, priority, status, dueDate.Value, category);
            }

            this.DialogResult = true;
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            TaskTitle = string.Empty;
            TaskDescription = string.Empty;
            SelectedPriority = "Medium";
            SelectedStatus = "Pending";
            SelectedCategoryId = null;
            DueDate = null;
            
            // Reset ComboBoxes
            SetComboBoxValues();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}