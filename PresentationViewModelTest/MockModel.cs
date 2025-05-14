using PresentationModel;
using System.Collections.ObjectModel;
using System.Numerics;

namespace PresentationViewModelTest
{
    internal class MockModel : IModel
    {
        public ObservableCollection<PresentationBall> Balls { get; } = new();
        public int AddBallCallCount { get; private set; }
        public Vector2 TableSize { get; private set; }
        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            AddBallCallCount++;
            Balls.Add(new PresentationBall(x, y, radius, color));
        }

        public void AddBall(float vx, float vy, string color)
        {
            Balls.Add(new PresentationBall(5.0f, 5.0f, 5.0f, color));
        }

        public Task AddBallAsync()
        {
            AddBallCallCount++;
            Balls.Add(new PresentationBall(10.0f, 10.0f, 5.0f, "DefaultColor"));
            return Task.CompletedTask;
        }

        public Task AddBallAsync(float x, float y, float vx, float vy, float radius, string color)
        {
            AddBallCallCount++;
            Balls.Add(new PresentationBall(x, y, radius, color));
            return Task.CompletedTask;
        }

        public float GetCanvasHeight()
        {
            return TableSize.Y;
        }

        public float GetCanvasWidth()
        {
            return TableSize.X;
        }

        public void InitializeScreenDimensions(float screenWidth, float screenHeight)
        {
            throw new NotImplementedException();
        }

        public void SetTableSize(float width, float height)
        {
            TableSize = new Vector2(width, height);
        }
    }
}
