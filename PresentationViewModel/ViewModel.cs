using PresentationModel;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PresentationViewModel
{
    public class ViewModel
    {
        private readonly string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple" };
        private readonly IModel _model;
        private readonly Random _rand = new Random();
        private bool _canGenerateBalls = true;

        public float ScreenWidth { get; private set; }
        public float ScreenHeight { get; private set; }

        public ObservableCollection<PresentationBall> Balls => _model.Balls;
        public ICommand GenerateBallsCommand { get; private set; }
        public int BallCount { get; set; } = 10;

        public ViewModel(IModel model)
        {
            _model = model;
            GenerateBallsCommand = new RelayCommand(ExecuteGenerateBalls, CanGenerateBalls);
        }

        private bool CanGenerateBalls(object parameter)
        {
            return _canGenerateBalls && BallCount > 0;
        }

        private void ExecuteGenerateBalls(object parameter)
        {
            GenerateBalls(BallCount, 60.0f, 400.0f);
            _canGenerateBalls = false;
            (GenerateBallsCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        public void InitializeScreenSize(float screenWidth, float screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
            _model.InitializeScreenDimensions(screenWidth, screenHeight);
        }

        public void SetTableSize(float width, float height)
        {
            _model.SetTableSize(width, height);
        }

        public void GenerateBalls(int count, float minVelocity, float maxVelocity)
        {
            if (count <= 0)
                throw new ArgumentException("Must be greater than 0", nameof(count));

            Balls.Clear();

            for (int i = 0; i < count; i++)
            {
                float vx = (_rand.NextSingle() * (maxVelocity - minVelocity) + minVelocity);
                float vy = (_rand.NextSingle() * (maxVelocity - minVelocity) + minVelocity);
                vx *= _rand.Next(2) == 0 ? 1 : -1;
                vy *= _rand.Next(2) == 0 ? 1 : -1;
                string color = colors[_rand.Next(colors.Length)];

                _model.AddBall(vx, vy, color);
            }
        }

        public float GetCanvasWidth() => _model.GetCanvasWidth();
        public float GetCanvasHeight() => _model.GetCanvasHeight();
    }
}