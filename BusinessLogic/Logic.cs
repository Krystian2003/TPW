using Data;
using System.Numerics;

namespace BusinessLogic
{
    public class Logic
    {
        private readonly List<Ball> _balls = new List<Ball>();

        public void InitializeBalls()
        {
            _balls.Clear();

            // delete or replace with th enew method
            _balls.Add(new Ball(100, 100, 15, "White")); 
            _balls.Add(new Ball(200, 200, 15, "Red"));   
            _balls.Add(new Ball(300, 300, 15, "Black")); 
        }

        public void UpdateBallPositions(float deltaTime)
        {
            foreach (var ball in _balls)
            {
                ball.Position += ball.Velocity * deltaTime;
            }
        }

        public void AddBall(float x, float y, float radius, string color)
        {
            _balls.Add(new Ball(x, y, radius, color));
        }

        public IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData()
        {
            return _balls.Select(b => (b.Position, b.Velocity, b.Radius, b.Color));
        }
    }
}