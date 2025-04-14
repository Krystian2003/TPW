﻿using PresentationModel;
using System.Collections.ObjectModel;

namespace PresentationViewModel
{
    public class ViewModel
    {
        private const double ReferenceWidth = 1920;
        private const double ReferenceHeight = 1080;
        private const float MinVelocity = 100.0f;
        private const float MaxVelocity = 400.0f;

        private double scale = 1;

        private readonly string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple" }; // Move this also?
        private readonly Model _ballManager;

        public ObservableCollection<PresentationBall> Balls => _ballManager.Balls;

        public ViewModel(Model ballManager)
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
