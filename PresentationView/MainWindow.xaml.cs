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
            _viewModel.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);

            //SizeChanged += MainWindow_SizeChanged;
        }

        //private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (canvas.IsLoaded)
        //    {
        //        _viewModel.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);
        //    }
        //}
    }
}