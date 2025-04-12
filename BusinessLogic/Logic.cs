using Data;

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

        public void UpdateBallPositions(double deltaTime)
        {
            foreach (var ball in _balls)
            {
                ball.X += 1 * deltaTime;
                ball.Y += 1 * deltaTime;
            }
        }

        public void AddBall(double x, double y, double radius, string color)
        {
            _balls.Add(new Ball(x, y, radius, color));
        }

        public IEnumerable<(double X, double Y, double Radius, string Color)> GetBallsData()
        {
            return _balls.Select(b => (b.X, b.Y, b.Radius, b.Color));
        }
    }
}