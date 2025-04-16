using PresentationModel;
using System.Collections.ObjectModel;

namespace PresentationViewModel
{
    public class ViewModel
    {
        private readonly string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple" };
        private readonly IModel _model;
        private readonly Random _rand = new Random();

        public ObservableCollection<PresentationBall> Balls => _model.Balls;

        public ViewModel(IModel model)
        {
            _model = model;
        }

        public void SetTableSize(float width, float height)
        {
            _model.SetTableSize(width, height);
        }

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _model.AddBall(x, y, vx, vy, radius, color);
        }

        public void GenerateBalls(int count, float canvasWidth, float canvasHeight, float scale, float minVelocity, float maxVelocity)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.", nameof(count));

            Balls.Clear();

            for (int i = 0; i < count; i++)
            {
                float radius = 25 * scale;
                float x = _rand.NextSingle() * (canvasWidth - radius * 2) + radius;
                float y = _rand.NextSingle() * (canvasHeight - radius * 2) + radius;
                float vx = (_rand.NextSingle() * (maxVelocity - minVelocity) + minVelocity) * scale;
                float vy = (_rand.NextSingle() * (maxVelocity - minVelocity) + minVelocity) * scale;
                vx *= _rand.Next(2) == 0 ? 1 : -1;
                vy *= _rand.Next(2) == 0 ? 1 : -1;
                string color = colors[_rand.Next(colors.Length)];
                AddBall(x, y, vx, vy, radius, color);
            }
        }
    }
}