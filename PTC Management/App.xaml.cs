using System.Windows;

namespace PTC_Management
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        // DONE: Сделать заставку
        protected override void OnStartup(StartupEventArgs e)
        {
            RunTime.Start();
            base.OnStartup(e);
        }


    }
}
