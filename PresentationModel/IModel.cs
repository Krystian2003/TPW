﻿using System.Collections.ObjectModel;

namespace PresentationModel
{
    public interface IModel
    {
        ObservableCollection<PresentationBall> Balls { get; }
        void SetTableSize(float width, float height);
        void InitializeScreenDimensions(float screenWidth, float screenHeight);
        Task AddBallAsync();
        Task AddBallAsync(float x, float y, float vx, float vy, float radius, string color);
        float GetCanvasWidth();
        float GetCanvasHeight();
    }
}