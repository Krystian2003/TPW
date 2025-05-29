using Data;
using System.Numerics;

namespace LogicTest
{
    internal class MockBallRepository : IBallRepository
    {
        private readonly List<Ball> _balls = [];

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            var ball = new Ball(x, y, vx, vy, radius, color);
            _balls.Add(ball);
        }

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
        }

        public void Clear()
        {
            _balls.Clear();
        }

        public IReadOnlyList<Ball> GetBalls()
        {
            return _balls.AsReadOnly();
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
