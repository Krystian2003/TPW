using BusinessLogic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Numerics;

namespace PresentationModel
{
    public class Model : IModel
    {
        private const double ReferenceWidth = 1920.0;
        private const double ReferenceHeight = 1080.0;
        private const double CanvasHeightRatio = 0.6;
        private const double AspectRatio = 16.0 / 9.0;

        private readonly ILogic _logic;
        private readonly Random _rand = new Random();
        private float _canvasWidth;
        private float _canvasHeight;
        private float _scale;

        public ObservableCollection<PresentationBall> Balls { get; } = new();

        public Model(ILogic logic)
        {
            _logic = logic;
            _logic.PositionsUpdated += OnPositionsUpdated;
            InitializeBalls();
            _logic.Start();
        }

        private void InitializeBalls()
        {
            var ballsData = _logic.GetBallsData();

            foreach (var ball in ballsData)
            {
                Balls.Add(new PresentationBall(
                    ball.Position.X,
                    ball.Position.Y,
                    ball.Radius,
                    ball.Color
                    ));
            }
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
            _canvasWidth = width;
            _canvasHeight = height;
            RecalculateScale();
            _logic.SetTableSize(_canvasWidth, _canvasHeight);
        }

        public void InitializeScreenDimensions(float screenWidth, float screenHeight)
        {
            _canvasHeight = (float)(screenHeight * CanvasHeightRatio);
            _canvasWidth = (float)(_canvasHeight * AspectRatio);

            RecalculateScale();
            _logic.SetTableSize(_canvasWidth, _canvasHeight);
        }

        private void RecalculateScale()
        {
            _scale = _canvasHeight / (float)ReferenceHeight;
        }

        public void AddBall(float vx, float vy, string color)
        {
            float radius = 25 * _scale;

            float x = _rand.NextSingle() * (_canvasWidth - radius * 2) + radius;
            float y = _rand.NextSingle() * (_canvasHeight - radius * 2) + radius;

            float scaledVx = vx * _scale;
            float scaledVy = vy * _scale;

            _logic.AddBall(x, y, scaledVx, scaledVy, radius, color);
            Balls.Add(new PresentationBall(x, y, radius, color));
        }

        public void AddBall(float x, float y, float vx, float vy, float radius, string color)
        {
            _logic.AddBall(x, y, vx, vy, radius, color);
            Balls.Add(new PresentationBall(x, y, radius, color));
        }

        public float GetCanvasWidth() => _canvasWidth;
        public float GetCanvasHeight() => _canvasHeight;
    }
}