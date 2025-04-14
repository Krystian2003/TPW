using BusinessLogic;
using Data;
using PresentationModel;
using PresentationViewModel;
using System.Windows;

namespace PresentationView;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IBallRepository repo = new BallRepository();
        ILogic logic = new Logic(repo); // change to the interface
        BallManager ballManager = new BallManager(logic);
        BilliardViewModel ballRenderer = new BilliardViewModel(ballManager);

        var dialog = new StartupDialog();
        if (dialog.ShowDialog() == true)
        {
            var mainWindow = new MainWindow(ballRenderer, dialog.BallCount);
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

