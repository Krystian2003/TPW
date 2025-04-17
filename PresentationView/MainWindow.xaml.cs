using PresentationViewModel;
using System.Windows;

namespace PresentationView
{
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel;

        public MainWindow(ViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.InitializeScreenSize(
                (float)SystemParameters.PrimaryScreenWidth,
                (float)SystemParameters.PrimaryScreenHeight
            );
            _viewModel.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);
        }
    }
}