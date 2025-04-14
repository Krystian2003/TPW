using System.Numerics;

namespace Data
{
    public class BallRepository : IBallRepository
    {
        private readonly List<Ball> _balls = new();

        public IReadOnlyList<Ball> GetBalls()
        {
            return _balls.AsReadOnly();
        }

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _balls.Add(new Ball(x, y, vx, vy, radius, color));
        }

        public void Clear()
        {
            _balls.Clear();
        }
        
        public void UpdateBallPosition(Ball ball, Vector2 newPosition)
        {
            var index = _balls.IndexOf(ball);
            if (index != -1)
            {
                _balls[index].Position = newPosition;
            }
        }

        public void UpdateBallVelocity(Ball ball, Vector2 newVelocity)
        {
            var index = _balls.IndexOf(ball);
            if (index != -1)
            {
                _balls[index].Velocity = newVelocity;
            }
        }
    }
}
