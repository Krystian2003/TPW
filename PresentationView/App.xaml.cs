using BusinessLogic;
using Data;
using PresentationModel;
using PresentationViewModel;
using System.Windows;

namespace PresentationView
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IBallRepository repo = new BallRepository();
            ILogic logic = new Logic(repo);
            Model model = new Model(logic);
            ViewModel viewModel = new ViewModel(model);

            var mainWindow = new MainWindow(viewModel);
            mainWindow.Closed += (s, args) => Shutdown();
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }
}