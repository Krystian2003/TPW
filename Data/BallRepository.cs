using System.Numerics;

namespace Data
{
    public class BallRepository : IBallRepository
    {
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
                _balls.Add(new Ball(x, y, vx, vy, radius, color));
            }
        }

        public void AddBall(Ball ball)
        {
            lock (_locker)
            {
                _balls.Add(ball);
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
                }
            }
        }
    }
}
