using Personal_Task_Manager.Models;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task = Personal_Task_Manager.Models.Task;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; } = new();
        public ObservableCollection<Task> FilteredTasks => Tasks;
        public ObservableCollection<Category> Categories { get; set; } = new();

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
            var service = new Services.TaskService();
            var tasks = await service.GetAllTasksAsync();

            Tasks.Clear();
            foreach (var task in tasks)
            {
                Tasks.Add(task);
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