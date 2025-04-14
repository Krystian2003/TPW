using System.Numerics;

namespace Data
{
    public interface IBallRepository // Maybe change the name
    {
        IReadOnlyList<Ball> GetBalls();
        void AddBall(float x, float y, float vx, float vy, float radius, string color);
        void Clear();
        void UpdateBallPosition(Ball ball, Vector2 newPosition);
        void UpdateBallVelocity(Ball ball, Vector2 newVelocity);
    }
}
