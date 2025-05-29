using System.Numerics;

namespace Data
{
    public class Ball
    {
        public int Id { get; }
        private static int _nextId;

        public Vector2 Position { get; internal set; }
        public Vector2 Velocity { get; internal set; }
        public float Radius { get; private set; }
        public string Color { get; private set; } 

        internal Ball(float x, float y, float vx, float vy, float radius, string color)
        {
            Id = Interlocked.Increment(ref _nextId);
            Position = new Vector2(x, y);
            Velocity = new Vector2(vx, vy);
            Radius = radius;
            Color = color;
        }

        public void UpdateVelocity(Vector2 newVelocity)
        {
            Velocity = newVelocity;
        }

        public void Move(float deltaTime)
        {
            Position += Velocity * deltaTime;
        }
    }
}