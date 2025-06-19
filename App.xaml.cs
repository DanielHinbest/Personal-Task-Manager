using Personal_Task_Manager.Data;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Personal_Task_Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
            }
        }
    }

}
