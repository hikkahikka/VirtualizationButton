using System.Configuration;
using System.Data;
using System.Windows;

namespace VirtualizationButton
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (!Environment.IsPrivilegedProcess)
            {
                MessageBox.Show("The program requires administrator rights to run",
                              "Error",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);

                Application.Current.Shutdown();
                return;
            }
            base.OnStartup(e);
        }
    }

}
