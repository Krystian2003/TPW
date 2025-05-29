using Logic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PresentationModel
{
    public class Model : IModel
    {
        private readonly string[] _colors = ["Red", "Blue", "Green", "Yellow", "Purple"];
        private const double ReferenceWidth = 1920.0;
        private const double ReferenceHeight = 1080.0;
        private const double CanvasHeightRatio = 0.6;
        private const double AspectRatio = 16.0 / 9.0;
        private const float MaxBallRadius = 50.0f;
        private const float MinBallRadius = 10.0f;
        private const float MaxVelocity = 600.0f;
        private const float MinVelocity = 200.0f;

        private readonly ILogic _logic;
        private readonly Random _rand = new Random();
        private float _canvasWidth;
        private float _canvasHeight;
        private float _scale;

        public ObservableCollection<PresentationBall> Balls { get; } = new();

        public Model(ILogic logic)
        {
            _logic = logic;
            _logic.PositionsUpdated += OnPositionsUpdated;
            InitializeBalls();
            _logic.StartAsync();
        }

        private void InitializeBalls()
        {
            var ballsData = _logic.GetBallsData();

            foreach (var ball in ballsData)
            {
                Balls.Add(new PresentationBall(
                    ball.Position.X,
                    ball.Position.Y,
                    ball.Radius,
                    ball.Color
                    ));
            }
        }

        private void OnPositionsUpdated(object? sender, EventArgs e)
        {
            var ballsData = _logic.GetBallsData().ToList();
            Application.Current.Dispatcher.Invoke(() =>
            {
                int count = Math.Min(Balls.Count, ballsData.Count);
                for (int i = 0; i < count; i++)
                {
                    Balls[i].ReferenceX = ballsData[i].Position.X / _scale;
                    Balls[i].ReferenceY = ballsData[i].Position.Y / _scale;
                    Balls[i].ReferenceRadius = ballsData[i].Radius / _scale;
                    Balls[i].UpdateScaledValues(_scale);
                    Balls[i].X = ballsData[i].Position.X;
                    Balls[i].Y = ballsData[i].Position.Y;
                }
            });
        }

        public void SetTableSize(float width, float height)
        {
            _canvasWidth = width;
            _canvasHeight = height;
            RecalculateScale();
            RescaleBalls();
            _logic.SetTableSize(_canvasWidth, _canvasHeight);
        }

        public void InitializeScreenDimensions(float screenWidth, float screenHeight)
        {
            _canvasHeight = (float)(screenHeight * CanvasHeightRatio);
            _canvasWidth = (float)(_canvasHeight * AspectRatio);

            RecalculateScale();
            RescaleBalls();
            _logic.SetTableSize(_canvasWidth, _canvasHeight);
        }

        private void RecalculateScale()
        {
            _scale = _canvasHeight / (float)ReferenceHeight;
        }

        private void RescaleBalls()
        {
            foreach (var ball in Balls)
            {
                ball.UpdateScaledValues(_scale);
            }
        }

        public async Task AddBallAsync()
        {
            float vx = (_rand.NextSingle() * (MaxVelocity - MinVelocity) + MinVelocity);
            float vy = (_rand.NextSingle() * (MaxVelocity - MinVelocity) + MinVelocity);
            vx *= _rand.Next(2) == 0 ? 1 : -1;
            vy *= _rand.Next(2) == 0 ? 1 : -1;
            string color = _colors[_rand.Next(_colors.Length)];

            float radius = _rand.NextSingle() * (MaxBallRadius - MinBallRadius) + MinBallRadius;
            float x, y;
            bool overlaps;
            do
            {
                x = _rand.NextSingle() * ((float)ReferenceWidth - 2 * radius) + radius;
                y = _rand.NextSingle() * ((float)ReferenceHeight - 2 * radius) + radius;
                overlaps = Balls.Any(b =>
                    Math.Sqrt((b.ReferenceX - x) * (b.ReferenceX - x) + (b.ReferenceY - y) * (b.ReferenceY - y)) < (b.ReferenceRadius + radius));
            } while (overlaps);

            float scaledX = x * _scale;
            float scaledY = y * _scale;
            float scaledVx = vx * _scale;
            float scaledVy = vy * _scale;
            float scaledRadius = radius * _scale;

            await _logic.AddBallAsync(scaledX, scaledY, scaledVx, scaledVy, scaledRadius, color);
            Balls.Add(new PresentationBall(x, y, radius, color) { X = scaledX, Y = scaledY, Radius = scaledRadius });
        }

        public async Task AddBallAsync(float x, float y, float vx, float vy, float radius, string color)
        {
            float scaledX = x * _scale;
            float scaledY = y * _scale;
            float scaledVx = vx * _scale;
            float scaledVy = vy * _scale;
            float scaledRadius = radius * _scale;
            await _logic.AddBallAsync(scaledX, scaledY, scaledVx, scaledVy, scaledRadius, color);
            Balls.Add(new PresentationBall(x, y, radius, color) { X = scaledX, Y = scaledY, Radius = scaledRadius });
        }

        public float GetCanvasWidth() => _canvasWidth;
        public float GetCanvasHeight() => _canvasHeight;
    }
}