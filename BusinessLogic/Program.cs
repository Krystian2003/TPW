using Data;

namespace BusinessLogic
{
    public class Logic
    {
        private readonly List<Ball> balls = new List<Ball>();

        public void InitializeBalls()
        {
            balls.Clear();

            balls.Add(new Ball(100, 100, 15, "White")); 
            balls.Add(new Ball(200, 200, 15, "Red"));   
            balls.Add(new Ball(300, 300, 15, "Black")); 
        }

        public IEnumerable<(double X, double Y, double Radius, string Color)> GetBallsData()
        {
            return balls.Select(b => (b.X, b.Y, b.Radius, b.Color));
        }
    }
}