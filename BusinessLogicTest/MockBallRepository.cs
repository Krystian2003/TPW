using Data;
using System.Numerics;

namespace BusinessLogicTest
{
    internal class MockBallRepository : IBallRepository
    {
        private readonly List<Ball> balls = new();

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            var ball = new Ball(x, y, vx, vy, radius, color);
            balls.Add(ball);
        }

        public void AddBall(Ball ball)
        {
            balls.Add(ball);
        }

        public void Clear()
        {
            balls.Clear();
        }

        public IReadOnlyList<Ball> GetBalls()
        {
            return balls.AsReadOnly();
        }

        public void UpdateBallPosition(Ball ball, Vector2 newPosition)
        {
            var index = balls.IndexOf(ball);
            if (index != -1)
            {
                balls[index].Position = newPosition;
            }
        }

        public void UpdateBallVelocity(Ball ball, Vector2 newVelocity)
        {
            var index = balls.IndexOf(ball);
            if (index != -1)
            {
                balls[index].Velocity = newVelocity;
            }
        }
    }
}
