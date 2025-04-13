using System.Configuration;
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
        private const double ReferenceWidth = 1920;
        private const double ReferenceHeight = 1080; // move both of these somewhere else
        private double uniformScale = 1;

        private readonly BilliardViewModel _ballRenderer = new BilliardViewModel();
        private readonly float _fixedDeltaTime = 1.0f / 60.0f;
        private double _accumulator = 0;
        private DateTime _lastFrameTime;
        private readonly List<Ellipse> _ballVisuals = new List<Ellipse>();
        private readonly int _ballCount;

        public MainWindow(int ballCount)
        {
            InitializeComponent();

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            
            // Change to const
            this.Width = screenWidth * 0.6;
            this.Height = screenHeight * 0.6;

            double scaleX = this.Width / ReferenceWidth;
            double scaleY = this.Height / ReferenceHeight;
            this.uniformScale = Math.Min(scaleX, scaleY); // possibly unnecessary

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _ballCount = ballCount;
            _lastFrameTime = DateTime.Now;
            CompositionTarget.Rendering += OnRendering;
            //SizeChanged += MainWindow_SizeChanged;
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
            float canvasWidth = (float)canvas.ActualWidth;
            float canvasHeight = (float)canvas.ActualHeight;

            for (int i = 0; i < count; i++)
            {
                float radius = 25 * (float)uniformScale;
                float x = rand.NextSingle() * (canvasWidth - radius  * 2) + radius;
                float y = rand.NextSingle() * (canvasHeight - radius * 2) + radius;
                float vx = (rand.NextSingle() * (400.0f - 100.0f) + 100.0f) * (float)uniformScale; // Change all magical numbers to constants
                float vy = vx;
                string color = colors[rand.Next(colors.Length)];
                _ballRenderer.CreateBall(x, y, vx, vy, radius, color);
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
        //protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        //{
        //    base.OnRenderSizeChanged(sizeInfo);

        //    double width = sizeInfo.NewSize.Width;
        //    double height = width / 2; // change the aspect ratio later
        //    this.Height = Height;
        //}
    }
}