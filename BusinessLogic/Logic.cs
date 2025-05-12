using Data;
using System.Numerics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BusinessLogic
{
    public class Logic : ILogic
    {
        public event EventHandler PositionsUpdated;
        public Vector2 TableSize { get; private set; } = new Vector2(800, 400);

        private readonly IBallRepository _ballRepository;
        private readonly Timer _updateTimer;
        private const float UpdateInterval = 16.67f;
        private const float DeltaTime = 0.01667f;

    public Logic(IBallRepository ballRepository)
        {
            _ballRepository = ballRepository;

            _updateTimer = new Timer(UpdateInterval);
            _updateTimer.Elapsed += OnTimerElapsed;
            _updateTimer.AutoReset = true;
        }

        public void Start()
        {
            _updateTimer.Start();
        }

        public void Stop()
        {
            _updateTimer.Stop();
        }

        internal void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateBallPositions(DeltaTime);
            PositionsUpdated?.Invoke(this, EventArgs.Empty);
        }

        //public void InitializeBalls()
        //{
        //    _ballRepository.Clear();
        //}

        public void SetTableSize(float width, float height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Table size must be positive.");

            TableSize = new Vector2(width, height);
        }

        public void UpdateBallPositions(float deltaTime)
        {
            var ballsCopy = _ballRepository.GetBalls().ToList();
            
            foreach (var ball in ballsCopy)
            {
                Vector2 newPosition = ball.Position + ball.Velocity * deltaTime;
                _ballRepository.UpdateBallPosition(ball, newPosition);
                BounceOffEdge(ball);
            }
        }

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _ballRepository.AddBall(x, y, vx, vy, radius, color);
        }

        public IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData()
        {
            return _ballRepository.GetBalls()
                .Select(b => (b.Position, b.Velocity, b.Radius, b.Color));
        }

        private void BounceOffEdge(Ball ball)
        {
            Vector2 minBounds = new Vector2(ball.Radius, ball.Radius);
            Vector2 maxBounds = new Vector2(TableSize.X - ball.Radius, TableSize.Y - ball.Radius);

            bool outOfBoundsX = ball.Position.X < minBounds.X || ball.Position.X > maxBounds.X;
            bool outOfBoundsY = ball.Position.Y < minBounds.Y || ball.Position.Y > maxBounds.Y;

            if (outOfBoundsX)
            {
                Vector2 newVelocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
                _ballRepository.UpdateBallVelocity(ball, newVelocity);
            }
            if (outOfBoundsY)
            {
                Vector2 newVelocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
                _ballRepository.UpdateBallVelocity(ball, newVelocity);
            }
        }
    }
}
