using BusinessLogic;

namespace PresentationModel
{
    public class BallManager
    {
        private readonly Logic _logic;

        public BallManager()
        {
            _logic = new Logic();
            _logic.InitializeBalls();
        }

        public IEnumerable<PresentationBall> GetBallsData()
        {
            return _logic.GetBallsData()
                   .Select(b => new PresentationBall(
                       x: b.X,
                       y: b.Y,
                       radius: b.Radius,
                       color: b.Color));
        }
    }
}
