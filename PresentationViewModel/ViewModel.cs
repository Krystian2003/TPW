using PresentationModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PresentationViewModel
{
    public class ViewModel
    {
        private const int MaxBallsAllowed = 100;
        private readonly IModel _model;
        private bool _canGenerateBalls = true;

        public ObservableCollection<PresentationBall> Balls => _model.Balls;
        public ICommand GenerateBallsCommand { get; }
        public int BallCount { get; set; } = 10;

        public ViewModel(IModel model)
        {
            _model = model;
            GenerateBallsCommand = new RelayCommand(ExecuteGenerateBalls, CanGenerateBalls);
        }

        private bool CanGenerateBalls(object? parameter)
        {
            return _canGenerateBalls && BallCount > 0;
        }

        private void ExecuteGenerateBalls(object? parameter)
        {
            Balls.Clear();
            _canGenerateBalls = false;
            (GenerateBallsCommand as RelayCommand)?.RaiseCanExecuteChanged();
            
            GenerateBalls(BallCount);
        }

        public void InitializeScreenSize(float screenWidth, float screenHeight)
        {
            _model.InitializeScreenDimensions(screenWidth, screenHeight);
        }

        public void SetTableSize(float width, float height)
        {
            _model.SetTableSize(width, height);
        }

        private void GenerateBalls(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Must be greater than 0", nameof(count));

            if (count > MaxBallsAllowed)
                throw new ArgumentException($"Cannot generate more than {MaxBallsAllowed} balls", nameof(count));

            Balls.Clear();

            for (int i = 0; i < count; i++)
            {
                _model.AddBallAsync();
            }
        }

        public float GetCanvasWidth() => _model.GetCanvasWidth();
        public float GetCanvasHeight() => _model.GetCanvasHeight();
    }
}