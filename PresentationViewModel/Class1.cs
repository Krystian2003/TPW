using PresentationModel;

namespace PresentationViewModel
{
    public class BallRenderer
    {
        private readonly BallManager _ballManager;

        public IEnumerable<PresentationBall> Balls => _ballManager.GetBallsData();

        public BallRenderer()
        {
            _ballManager = new BallManager();
        }
    }
}
