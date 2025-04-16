using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        public event EventHandler PositionsUpdated;

        public void Start() => StartCalled = true;
        public void Stop() => StopCalled = true;

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
            => AddedBalls.Add((x, y, vx, vy, radius, color));

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
