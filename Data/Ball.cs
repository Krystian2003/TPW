using System.Numerics;

namespace Data
{
    public class Ball
    {
        public Vector2 Position { get; internal set; }
        public Vector2 Velocity { get; internal set; }
        // Might change radius and color setters to internal later
        public float Radius { get; private set; }
        public string Color { get; private set; } 

        internal Ball(float x, float y, float vx, float vy, float radius, string color)
        {
            Position = new Vector2(x, y);
            Velocity = new Vector2(vx, vy);
            Radius = radius;
            Color = color;
        }

        public void UpdateVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
        }
    }
}