using BusinessLogic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationModel
{
    public class BallManager : INotifyPropertyChanged
    {
        private readonly Logic _logic;
        public ObservableCollection<PresentationBall> Balls { get; } = new ObservableCollection<PresentationBall>();

        public BallManager()
        {
            _logic = new Logic();
            InitializeBalls();
        }

        private void InitializeBalls()
        {
            var ballsData = _logic.GetBallsData();

            foreach (var ball in ballsData)
            {
                Balls.Add(new PresentationBall(
                    ball.X,
                    ball.Y,
                    ball.Radius,
                    ball.Color
                    ));
            }
        }

        public void UpdatePositions()
        {
            _logic.UpdateBallPositions();
            var ballsData = _logic.GetBallsData().ToList();

            for (int i = 0; i < ballsData.Count; i++)
            {
                Balls[i].X = ballsData[i].X;
                Balls[i].Y = ballsData[i].Y;
            }
        }
        public void AddBall(double x, double y, double radius, string color)
        {
            _logic.AddBall(x, y, radius, color);

            Balls.Add(new PresentationBall(x, y, radius, color));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}