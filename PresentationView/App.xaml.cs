using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media.Animation;

namespace PresentationView;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var dialog = new StartupDialog();
        if (dialog.ShowDialog() == true)
        {
            var mainWindow = new MainWindow(dialog.BallCount);
            mainWindow.Closed += (s, args) => Shutdown();
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }
        else
        {
            Shutdown();
        }
    }
}

