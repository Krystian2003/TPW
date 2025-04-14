using PresentationViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PresentationView
{
    public partial class MainWindow : Window
    {
        private readonly ViewModel _ballRenderer;
        private readonly float _fixedDeltaTime = 1.0f / 60.0f;
        private double _accumulator = 0;
        private DateTime _lastFrameTime;
        private readonly int _ballCount;

        public MainWindow(ViewModel ballRenderer, int ballCount)
        {
            InitializeComponent();

            _ballRenderer = ballRenderer;
            _ballCount = ballCount;

            _lastFrameTime = DateTime.Now;
            CompositionTarget.Rendering += OnRendering;
        }
        private void OnRendering(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastFrameTime).TotalSeconds;
            _lastFrameTime = now;

            _accumulator += deltaTime;

            while (_accumulator >= _fixedDeltaTime)
            {
                _ballRenderer.Update(_fixedDeltaTime);
                _accumulator -= _fixedDeltaTime;
            }
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Update the table size when the window is resized
            _ballRenderer.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Possibly move the cast to logic instead
            _ballRenderer.SetTableSize((float)canvas.ActualWidth, (float)canvas.ActualHeight);
            _ballRenderer.GenerateBalls(_ballCount, (float)canvas.ActualWidth, (float)canvas.ActualHeight, 0.6f, 100.0f, 400.0f); // move scale to viewmodel

            this.DataContext = _ballRenderer;
        }
    }
}