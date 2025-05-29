using System.Numerics;

namespace Data
{
    public class BallRepository : IBallRepository
    {
        private readonly BallLogger _logger = new("ball_data.log");
        private readonly List<Ball> _balls = new();
        private readonly object _locker = new();

        public IReadOnlyList<Ball> GetBalls()
        {
            lock (_locker)
            {
                return _balls.ToList().AsReadOnly();
            }
        }

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            lock (_locker)
            {
                var ball = new Ball(x, y, vx, vy, radius, color);
                AddBall(ball);
            }
        }

        public void AddBall(Ball ball)
        {
            lock (_locker)
            {
                _balls.Add(ball);
                _logger.Log($"ADD pos={ball.Position.X},{ball.Position.Y} " +
                            $"vel={ball.Velocity.X},{ball.Velocity.Y} " +
                            $"r={ball.Radius} c={ball.Color}", ball.Id);
            }
        }

        public void Clear()
        {
            lock (_locker)
            {
                _balls.Clear();
            }
        }
        
        public void UpdateBallPosition(Ball ball, Vector2 newPosition)
        {
            lock (_locker)
            {
                var index = _balls.IndexOf(ball);
                if (index != -1)
                {
                    _balls[index].Position = newPosition;
                    _logger.Log($"NEW POS {newPosition.X},{newPosition.Y}", ball.Id);
                }
            }
        }

        public void UpdateBallVelocity(Ball ball, Vector2 newVelocity)
        {
            lock (_locker)
            {
                var index = _balls.IndexOf(ball);
                if (index != -1)
                {
                    _balls[index].UpdateVelocity(newVelocity);
                    _logger.Log($"NEW VEL ={newVelocity.X},{newVelocity.Y}", ball.Id);
                }
            }
        }
    }
}
