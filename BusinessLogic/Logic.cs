using Data;
using System.Numerics;

namespace BusinessLogic
{
    public class Logic
    {
        public Vector2 TableSize { get; set; } = new Vector2(800, 400);

        private readonly IBallRepository _ballRepository;

        public Logic(IBallRepository ballRepository)
        {
            _ballRepository = ballRepository;
        }
        public void InitializeBalls()
        {
            _ballRepository.Clear();
        }

        public void SetTableSize(float width, float height)
        {
            {
                TableSize = new Vector2(width, height);
            }
        }

        public void UpdateBallPositions(float deltaTime)
        {
            foreach (var ball in _ballRepository.GetBalls())
            {
                ball.Position += ball.Velocity * deltaTime;

                BounceOffEdge(ball);
            }
        }

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _ballRepository.AddBall(new Ball(x, y, vx, vy, radius, color));
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

            if (outOfBoundsX) ball.Velocity = new Vector2(-ball.Velocity.X, ball.Velocity.Y);
            if (outOfBoundsY) ball.Velocity = new Vector2(ball.Velocity.X, -ball.Velocity.Y);
        }
    }
}