namespace Data
{
    public class BallRepository : IBallRepository
    {
        private readonly List<Ball> _balls = new List<Ball>();

        public IReadOnlyList<Ball> GetBalls()
        {
            return _balls.AsReadOnly();
        }

        public void AddBall(Ball ball)
        {
            _balls.Add(ball);
        }

        public void Clear()
        {
            _balls.Clear();
        }
    }
}
