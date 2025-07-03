using Personal_Task_Manager.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

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
    }
}