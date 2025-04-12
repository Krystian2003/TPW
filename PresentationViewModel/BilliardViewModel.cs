using PresentationModel;
using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;

namespace PresentationViewModel
{
    // rename maybe
    public class BilliardViewModel
    {
        private readonly BallManager _ballManager;
        private readonly System.Timers.Timer _updateTimer;

        public ObservableCollection<PresentationBall> Balls => _ballManager.Balls;

        public BilliardViewModel()
        {
            _ballManager = new BallManager();

            _updateTimer = new System.Timers.Timer(16);
            _updateTimer.Start();
        }

        public void Update()
        {
            _ballManager.UpdatePositions();
        }

        public void CreateBall(double x, double y, double radius, string color)
        {
            _ballManager.AddBall(x, y, radius, color);
        }
    }
}
