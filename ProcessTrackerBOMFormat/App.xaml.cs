using System.Windows;

namespace Formatter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Title = "Process Tracker BOM Format";

            mainWindow.Show();
        }

    }
}
