using Data;
using System.Numerics;

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
            var balls = _ballRepository.GetBalls().ToList();

            foreach (var ball in balls)
            {
                Vector2 newPos = ball.Position + ball.Velocity * deltaTime;
                Vector2 newVel = ball.Velocity;

                float minX = ball.Radius;
                float maxX = TableSize.X - ball.Radius;
                float minY = ball.Radius;
                float maxY = TableSize.Y - ball.Radius;

                if (newPos.X < minX || newPos.X > maxX)
                {
                    newVel.X = -newVel.X;
                    newPos.X = Math.Clamp(newPos.X, minX, maxX);
                }

                if (newPos.Y < minY || newPos.Y > maxY)
                {
                    newVel.Y = -newVel.Y;
                    newPos.Y = Math.Clamp(newPos.Y, minY, maxY);
                }

                if (Math.Abs(newPos.X - 49.5f) < 0.1f) newPos.X = 49.5f;

                _ballRepository.UpdateBallVelocity(ball, newVel);
                _ballRepository.UpdateBallPosition(ball, newPos);
            }

            for (int i = 0; i < balls.Count; i++)
                for (int j = i + 1; j < balls.Count; j++)
                {
                    var b1 = balls[i];
                    var b2 = balls[j];
                    Vector2 delta = b1.Position - b2.Position;
                    float dist = delta.Length();
                    float distSq = dist * dist;
                    float radii = b1.Radius + b2.Radius;

                    if (distSq <= radii * radii)
                    {
                    BallCollision(b1, b2);

                    if (dist > 0f)
                    {
                        float overlap = radii - dist;
                        Vector2 n = delta / dist;  
                        Vector2 p1 = b1.Position +  n * (overlap / 2.0f);
                        Vector2 p2 = b2.Position -  n * (overlap / 2.0f);
                        _ballRepository.UpdateBallPosition(b1, p1);
                        _ballRepository.UpdateBallPosition(b2, p2);
                    }
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
    }
}
