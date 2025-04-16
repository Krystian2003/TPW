using PresentationViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PresentationView
{
    public partial class MainWindow : Window
    {
        private readonly ViewModel _ballRenderer;
        private readonly int _ballCount;

        public MainWindow(ViewModel ballRenderer, int ballCount)
        {
            InitializeComponent();

            _ballRenderer = ballRenderer;
            _ballCount = ballCount;
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (canvas.IsLoaded)
            {
                _ballRenderer.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ballRenderer.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);
            _ballRenderer.GenerateBalls(_ballCount, (float)canvas.ActualWidth, (float)canvas.ActualHeight, 0.6f, 100.0f, 400.0f);
            this.DataContext = _ballRenderer;
        }
    }
}