using System.Numerics;

namespace BusinessLogic
{
    public interface ILogic
    {
        Vector2 TableSize { get; }

        void SetTableSize(float widhth, float height);
        void InitializeBalls();
        void AddBall(float x, float y, float vx, float vy, float radius, string color);
        void UpdateBallPositions(float deltaTime);
        IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData(); // maybe change this?
    }
}
