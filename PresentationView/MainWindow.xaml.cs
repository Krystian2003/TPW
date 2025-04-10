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
        private readonly BallRenderer _ballRenderer;
        private readonly DispatcherTimer _timer;
        private readonly List<Ellipse> _ballVisuals = new List<Ellipse>();

        public MainWindow()
        {
            InitializeComponent();

            _ballRenderer = new BallRenderer();

            InitializeBallVisuals();

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            _timer.Tick += UpdateBalls;
            _timer.Start();
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

        private void UpdateBalls(object sender, EventArgs e)
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
    }
}