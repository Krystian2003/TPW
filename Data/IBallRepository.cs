using System.Numerics;

namespace Data
{
    public interface IBallRepository
    {
        IReadOnlyList<Ball> GetBalls();
        void AddBall(float x, float y, float vx, float vy, float radius, string color);
        void AddBall(Ball ball);
        void Clear();
        void UpdateBallPosition(Ball ball, Vector2 newPosition);
        void UpdateBallVelocity(Ball ball, Vector2 newVelocity);
    }
}
