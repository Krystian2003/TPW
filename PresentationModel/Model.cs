using BusinessLogic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace PresentationModel
{
    public class Model : INotifyPropertyChanged
    {
        private readonly ILogic _logic;
        public ObservableCollection<PresentationBall> Balls { get; } = new();

        public Model(ILogic logic)
        {
            _logic = logic;
            _logic.PositionsUpdated += OnPositionsUpdated;
            InitializeBalls();
        }

        private void InitializeBalls()
        {
            var ballsData = _logic.GetBallsData();

            foreach (var ball in ballsData)
            {
                Balls.Add(new PresentationBall(
                    ball.Position.X,
                    ball.Position.Y,
                    // Should the view ball object have velocity?
                    ball.Radius,
                    ball.Color
                    ));
            }
            _logic.Start();
        }

        private void OnPositionsUpdated(object? sender, EventArgs e)
        {
                var ballsData = _logic.GetBallsData().ToList();
                for (int i = 0; i < ballsData.Count; i++)
                {
                    Balls[i].X = ballsData[i].Position.X;
                    Balls[i].Y = ballsData[i].Position.Y;
                }
        }

        public void SetTableSize(float width, float height)
        {
            _logic.SetTableSize(width, height);
        }

        public void UpdatePositions(float deltaTime)
        {
            var ballsData = _logic.GetBallsData().ToList();

            // Possibly change to foreach
            for (int i = 0; i < ballsData.Count; i++)
            {
                Balls[i].X = ballsData[i].Position.X;
                Balls[i].Y = ballsData[i].Position.Y;
            }
        }
        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _logic.AddBall(x, y, vx, vy, radius, color);

            Balls.Add(new PresentationBall(x, y, radius, color));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}