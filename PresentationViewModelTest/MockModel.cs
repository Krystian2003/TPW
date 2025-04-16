using PresentationModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
        public void SetTableSize(float width, float height)
        {
            TableSize = new Vector2(width, height);
        }
    }
}
