using PresentationModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PresentationViewModel
{
    // rename maybe
    public class BilliardViewModel
    {
        private readonly BallManager _ballManager;

        public ObservableCollection<PresentationBall> Balls => _ballManager.Balls;

        public BilliardViewModel()
        {
            _ballManager = new BallManager();
        }

        public void Update(double deltaTime)
        {
            _ballManager.UpdatePositions(deltaTime);
        }

        public void CreateBall(double x, double y, double radius, string color)
        {
            _ballManager.AddBall(x, y, radius, color);
        }
    }
}
