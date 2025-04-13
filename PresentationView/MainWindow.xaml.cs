using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using PresentationViewModel;

namespace PresentationView
{
    public partial class MainWindow : Window
    {

        private readonly BilliardViewModel _ballRenderer = new BilliardViewModel();
        private readonly float _fixedDeltaTime = 1.0f / 60.0f;
        private double _accumulator = 0;
        private DateTime _lastFrameTime;
        private readonly List<Ellipse> _ballVisuals = new List<Ellipse>();
        private readonly int _ballCount;

        public MainWindow(int ballCount)
        {
            InitializeComponent();
            _ballCount = ballCount;
            _lastFrameTime = DateTime.Now;
            CompositionTarget.Rendering += OnRendering;
            SizeChanged += MainWindow_SizeChanged;
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

            UpdateBalls();
        }

        private void InitializeBallVisuals()
        {
            canvas.Children.Clear();
            _ballVisuals.Clear();

            foreach (var ball in _ballRenderer.Balls)
            {
                var ellipse = new Ellipse
                {
                    Width = ball.Radius * 2,
                    Height = ball.Radius * 2,
                    Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ball.Color)),
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };

                Canvas.SetLeft(ellipse, ball.X - ball.Radius);
                Canvas.SetTop(ellipse, ball.Y - ball.Radius);

                canvas.Children.Add(ellipse);
                _ballVisuals.Add(ellipse);
            }
        }
        
        // should this be here?
        private void UpdateBalls()
        {
            var balls = _ballRenderer.Balls.ToArray();

            for (int i = 0; i < balls.Length && i < _ballVisuals.Count; i++)
            {
                var ball = balls[i];
                var visual = _ballVisuals[i];

                Canvas.SetLeft(visual, ball.X - ball.Radius);
                Canvas.SetTop(visual, ball.Y - ball.Radius);
            }
        }
        // should this be here?
        private void GenerateBalls(int count)
        {
            _ballRenderer.Balls.Clear();
            Random rand = new Random();
            string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple" };

            for (int i = 0; i < count; i++)
            {
                float x = rand.NextSingle() * (float)(canvas.ActualWidth - 20) + 10;
                float y = rand.NextSingle() * (float)(canvas.ActualHeight - 20) + 10;
                float vx = rand.NextSingle() * (200.0f - 50.0f) + 50.0f; // Change all magical numbers to constants
                float vy = vx;
                string color = colors[rand.Next(colors.Length)];
                _ballRenderer.CreateBall(x, y, vx, vy, 10, color);
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
            GenerateBalls(_ballCount);
            InitializeBallVisuals();
        }


    }
}