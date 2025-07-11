using Personal_Task_Manager.Models;
using Personal_Task_Manager.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Task = System.Threading.Tasks.Task;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for AddEditTaskWindow.xaml
    /// </summary>
    public partial class AddEditTaskWindow : Window
    {
        public ObservableCollection<Category> Categories { get; set; } = new();
        private int? _pendingCategoryId;

        public AddEditTaskWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += AddEditTaskWindow_Loaded;
        }

        public AddEditTaskWindow(Personal_Task_Manager.Models.Task taskToEdit)
    : this()
        {
            txtTaskTitle.Text = taskToEdit.Title;
            txtTaskDescription.Text = taskToEdit.Description;
            cmbPriority.Text = taskToEdit.Priority;
            cmbStatus.Text = taskToEdit.Status;
            dateDueDate.SelectedDate = taskToEdit.DueDate;
            cmbCategories.SelectedValue = taskToEdit.CategoryId;

            IsEditMode = true;
            EditingTaskId = taskToEdit.Id;
            _originalCreatedAt = taskToEdit.CreatedAt;
            _pendingCategoryId = taskToEdit.CategoryId;
        }

        public bool IsEditMode { get; set; } = false;
        public int EditingTaskId { get; set; } = 0;
        private DateTime _originalCreatedAt;

        private async void AddEditTaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategoriesAsync();
            if (_pendingCategoryId.HasValue)
            {
                cmbCategories.SelectedValue = _pendingCategoryId;
                _pendingCategoryId = null;
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
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            CategoryService categoryService = new CategoryService();
            string title = txtTaskTitle.Text;
            string description = txtTaskDescription.Text;
            string priority = cmbPriority.Text;
            string status = cmbStatus.Text;
            DateTime? dueDate = dateDueDate.SelectedDate;

            if (string.IsNullOrWhiteSpace(title) || dueDate == null)
            {
                MessageBox.Show("Please enter a title and select a due date.");
                return;
            }

            var category = await categoryService.GetCategoryByNameAsync(cmbCategories.Text);
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
            txtTaskTitle.Clear();
            txtTaskDescription.Clear();
            cmbPriority.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            cmbCategories.SelectedIndex = -1;
            dateDueDate.SelectedDate = null;
        }
    }
}