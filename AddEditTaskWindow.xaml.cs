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

        public AddEditTaskWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += AddEditTaskWindow_Loaded;
        }

        private async void AddEditTaskWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategoriesAsync();
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
            await taskService.AddTaskAsync(title, description, priority, status, dueDate.Value, category);
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