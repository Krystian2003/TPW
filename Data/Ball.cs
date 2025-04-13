﻿using System.Numerics;

namespace Data
{
    public class Ball
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        // should radius be a float?
        public float Radius { get; set; }
        public string Color { get; set; } 

        public Ball(float x, float y, float vx, float vy, float radius, string color)
        {
            Position = new Vector2(x, y);
            Velocity = new Vector2(vx, vx);
            Radius = radius;
            Color = color;
        }
    }
}