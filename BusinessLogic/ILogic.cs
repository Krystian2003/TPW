using System.Numerics;

namespace BusinessLogic
{
    public interface ILogic
    {
        Vector2 TableSize { get; }

        event EventHandler? PositionsUpdated;
        Task StartAsync();
        Task StopAsync();
        Task AddBallAsync(float x, float y, float vx, float vy, float radius, string color);

        void SetTableSize(float width, float height);
        IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData();
    }
}
