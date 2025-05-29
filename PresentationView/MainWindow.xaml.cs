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

            Canvas.SizeChanged += Canvas_SizeChanged;
        }

        private void Canvas_SizeChanged(object? sender, SizeChangedEventArgs e)
        {
            _viewModel.SetTableSize(
                (float)Canvas.ActualWidth,
                (float)Canvas.ActualHeight
            );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.InitializeScreenSize(
                (float)SystemParameters.PrimaryScreenWidth,
                (float)SystemParameters.PrimaryScreenHeight
            );
            _viewModel.SetTableSize((float)Canvas.ActualWidth, (float)Canvas.ActualHeight);
        }
    }
}