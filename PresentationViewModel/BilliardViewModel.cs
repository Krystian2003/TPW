using PresentationModel;
using System.Collections.ObjectModel;

namespace PresentationViewModel
{
    // rename maybe
    public class BilliardViewModel
    {
        private readonly string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple" }; // Move this also?
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

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _ballManager.AddBall(x, y, vx, vy, radius, color);
        }

        public void GenerateBalls(int count, float canvasWidth, float canvasHeight, float scale, float minVelocity, float maxVelocity)
        {
            Balls.Clear();
            Random rand = new Random(); // should random be here or..

            for (int i = 0; i < count; i++)
            {
                float radius = 25 * scale;
                float x = rand.NextSingle() * (canvasWidth - radius * 2) + radius;
                float y = rand.NextSingle() * (canvasHeight - radius * 2) + radius;
                // move applying scale to `Model`
                float vx = (rand.NextSingle() * (maxVelocity - minVelocity) + minVelocity) * scale;
                float vy = vx;
                // Randomize direction
                vx = vx * ((rand.Next(2) == 0) ? 1 : -1);
                vy = vy * ((rand.Next(2) == 0) ? 1 : -1);
                string color = colors[rand.Next(colors.Length)];
                AddBall(x, y, vx, vy, radius, color);
            }
        }
    }
}
