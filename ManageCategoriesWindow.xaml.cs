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
    /// Interaction logic for ManageCategoriesWindow.xaml
    /// </summary>
    public partial class ManageCategoriesWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Category> Categories { get; set; } = new();
        private Category? _selectedCategory;

        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                OnPropertyChanged(nameof(CanEdit));
                OnPropertyChanged(nameof(CanDelete));
            }
        }

        public bool CanEdit => SelectedCategory != null;
        public bool CanDelete => SelectedCategory != null;

        public ManageCategoriesWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += ManageCategoriesWindow_Loaded;
        }

        private async void ManageCategoriesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadCategoriesAsync();
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var service = new CategoryService();
                var categories = await service.GetAllCategoriesAsync();

                Categories.Clear();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            NewCategoryDialog dialog = new NewCategoryDialog();
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                await LoadCategoriesAsync();
            }
        }

        private async void btnEditCategory_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory == null) return;

            EditCategoryDialog dialog = new EditCategoryDialog(SelectedCategory);
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                await LoadCategoriesAsync();
            }
        }

        private async void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCategory == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete the category '{SelectedCategory.Name}'?\n\nThis action cannot be undone and may affect existing tasks.",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var service = new CategoryService();
                    await service.DeleteCategoryAsync(SelectedCategory.Id);
                    await LoadCategoriesAsync();
                    SelectedCategory = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}