using Data;
using System.Numerics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace BusinessLogic
{
    public class Logic : ILogic
    {
        public event EventHandler? PositionsUpdated;
        public Vector2 TableSize { get; private set; } = new Vector2(800, 400);

        private readonly IBallRepository _ballRepository;
        private CancellationTokenSource? _cts;
        private readonly object _locker = new();

        private const int UpdateInterval = 17;
        private const float DeltaTime = 0.01667f;

        public Logic()
        {
            _ballRepository = new BallRepository();

        }

        public Task StartAsync()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
                return Task.CompletedTask;

            _cts = new CancellationTokenSource();
            return Task.Run(UpdateLoopAsync, _cts.Token);
        }

        public Task StopAsync()
        {
            _cts?.Cancel();
            return Task.CompletedTask;
        }

        private async Task UpdateLoopAsync()
        {
            var token = _cts!.Token;
            while (!token.IsCancellationRequested)
            {
                lock (_locker)
                {
                    UpdateBallPositions(DeltaTime);
                }
                PositionsUpdated?.Invoke(this, EventArgs.Empty);
                try { await Task.Delay(UpdateInterval, token); }
                catch (TaskCanceledException) { break; }
            }
        }

        public Task AddBallAsync(float x, float y, float vx, float vy, float radius, string color)
        {
            return Task.Run(() =>
            {
                lock (_locker)
                    _ballRepository.AddBall(x, y, vx, vy, radius, color);
            });
        }

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

            for (int i = 0; i < ballsCopy.Count; i++)
                for (int j = i + 1; j < ballsCopy.Count; j++)
                {
                    var ball1 = ballsCopy[i];
                    var ball2 = ballsCopy[j];

                    float distanceSquared = Vector2.DistanceSquared(ball1.Position, ball2.Position);
                    float radiusSum = ball1.Radius + ball2.Radius;
                    float radiusSumSquared = radiusSum * radiusSum;

                    if (distanceSquared <= radiusSumSquared)
                    {
                        BallCollision(ball1, ball2);
                    }
                }
        }

        public IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData()
        {
            return _ballRepository.GetBalls()
                .Select(b => (b.Position, b.Velocity, b.Radius, b.Color));
        }

        private void BallCollision(Ball ball1, Ball ball2)
        {
            float mass1 = ball1.Radius * ball1.Radius;
            float mass2 = ball2.Radius * ball2.Radius;

            Vector2 posDiff = ball1.Position - ball2.Position;
            float distanceSquared = posDiff.LengthSquared();

            if (distanceSquared == 0f)
                return;

            Vector2 velDiff1 = ball1.Velocity - ball2.Velocity;
            float dotProduct = Vector2.Dot(posDiff, velDiff1);

            if(dotProduct >= 0)
                return;

            Vector2 impulse = (2 * dotProduct / (mass1 + mass2)) * posDiff / distanceSquared;

            _ballRepository.UpdateBallVelocity(ball1, ball1.Velocity - impulse * mass2);
            _ballRepository.UpdateBallVelocity(ball2, ball2.Velocity + impulse * mass1);
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
