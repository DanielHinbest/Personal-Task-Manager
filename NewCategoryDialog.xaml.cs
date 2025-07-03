using Personal_Task_Manager.Data;
using Personal_Task_Manager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for NewCategoryDialog.xaml
    /// </summary>
    public partial class NewCategoryDialog : Window
    {
        public NewCategoryDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtInput.Focus();
        }

        private async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string category = txtInput.Text;
            CategoryService categoryService = new CategoryService();
            await categoryService.AddCategoryAsync(category);
            this.DialogResult = true;
            this.Close();
        }
    }
}
