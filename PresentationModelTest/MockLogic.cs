using Logic;
using System.Numerics;

namespace PresentationModelTest
{
    internal class MockLogic : ILogic
    {
        public bool StartCalled { get; private set; } = false;
        public bool StopCalled { get; private set; } = false;
        public List<(float x, float y, float vx, float vy, float radius, string color)> AddedBalls { get; } = 
            new List<(float x, float y, float vx, float vy, float radius, string color)>();
        public List<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> BallsData { get; } = new();
        public Vector2 TableSize { get; set; } = new Vector2(800, 400);

        public event EventHandler? PositionsUpdated;

        public Task StartAsync()
        {
            StartCalled = true;
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            StopCalled = true;
            return Task.CompletedTask;
        }

        public Task AddBallAsync(float x, float y, float vx, float vy, float radius, string color)
        {
            AddedBalls.Add((x, y, vx, vy, radius, color));
            BallsData.Add((new Vector2(x,y), new Vector2(vx,vy), radius, color));
            return Task.CompletedTask;
        }

        public IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData()
        {
            return BallsData;
        }

        public void SetTableSize(float width, float height)
        {
            TableSize = new Vector2(width, height);
        }

        public void RaisePositionsUpdated()
            => PositionsUpdated?.Invoke(this, EventArgs.Empty);
    }
}
