namespace Data
{
    public interface IBallRepository // Maybe change the name
    {
        IReadOnlyList<Ball> GetBalls();
        void AddBall(Ball ball);
        void Clear();
    }
}
