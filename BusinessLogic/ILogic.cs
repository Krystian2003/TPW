﻿using System.Numerics;

namespace BusinessLogic
{
    public interface ILogic
    {
        event EventHandler PositionsUpdated;

        Vector2 TableSize { get; }

        void SetTableSize(float width, float height);
        // Not sure if needed
        //void InitializeBalls();
        void Start();
        void Stop();
        void AddBall(float x, float y, float vx, float vy, float radius, string color);
        IEnumerable<(Vector2 Position, Vector2 Velocity, float Radius, string Color)> GetBallsData(); // maybe change this?
    }
}
