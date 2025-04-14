using PresentationModel;
using System.Collections.ObjectModel;

namespace PresentationViewModel
{
    // rename maybe
    public class BilliardViewModel
    {
        private readonly BallManager _ballManager;

        public ObservableCollection<PresentationBall> Balls => _ballManager.Balls;

        public BilliardViewModel(BallManager ballManager)
        {
            _ballManager = ballManager;
        }

        public void SetTableSize(float width, float height)
        {
            _ballManager.SetTableSize(width, height);
        }

        public void Update(float deltaTime)
        {
            _ballManager.UpdatePositions(deltaTime);
        }

        public void CreateBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _ballManager.AddBall(x, y, vx, vy, radius, color);
        }
    }
}
