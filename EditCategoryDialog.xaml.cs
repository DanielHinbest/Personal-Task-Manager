using Personal_Task_Manager.Models;
using Personal_Task_Manager.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for EditCategoryDialog.xaml
    /// </summary>
    public partial class EditCategoryDialog : Window, INotifyPropertyChanged
    {
        private readonly Category _originalCategory;
        private string _categoryName = string.Empty;

        public string CategoryName
        {
            get => _categoryName;
            set
            {
                _categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
                OnPropertyChanged(nameof(CanSave));
            }
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(CategoryName) && CategoryName.Trim() != _originalCategory.Name;

        public EditCategoryDialog(Category category)
        {
            InitializeComponent();
            this.DataContext = this;
            _originalCategory = category;
            CategoryName = category.Name;
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCategoryName.Focus();
            txtCategoryName.SelectAll();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CanSave) return;

            try
            {
                btnSave.IsEnabled = false;
                
                var service = new CategoryService();
                
                // Create a copy of the category with the updated name
                var updatedCategory = new Category
                {
                    Id = _originalCategory.Id,
                    Name = CategoryName.Trim(),
                    CreatedAt = _originalCategory.CreatedAt,
                    Tasks = _originalCategory.Tasks
                };
                
                await service.UpdateCategoryAsync(updatedCategory);
                
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating category: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnSave.IsEnabled = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}