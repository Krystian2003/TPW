using System.Collections.Concurrent;
using System.Numerics;

namespace Data;

public interface IBallLogger
{
    public void Log(Ball ball);
    public void Log(Ball ball1, Ball ball2);
    public void LogPosition(Ball ball1, Vector2 newPosition);
    public void LogVelocity(Ball ball1, Vector2 newVelocity);
    public void LogWallXCollision(Ball ball);
    public void LogWallYCollision(Ball ball);
}