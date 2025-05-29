using System.Collections.Concurrent;
using System.Text;
using System.IO;
using System;
using System.Numerics;

namespace Data;

public class BallLogger : IDisposable
{
    private readonly BlockingCollection<string> _queue = new();
    private readonly Task _writerTask;
    private readonly string _path;

    public BallLogger(string directory)
    {
        Directory.CreateDirectory(directory);
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
        this._path = Path.Combine(directory, $"ball_data_{timestamp}.log");
        _writerTask = Task.Run(BackgroundWrite);
    }

    // Ball added
    public void Log(Ball ball)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] ADD pos={ball.Position.X},{ball.Position.Y} " +
            $"vel={ball.Velocity.X},{ball.Velocity.Y} " +
            $"r={ball.Radius} c={ball.Color}");
    }

    // Collision
    public void Log(Ball ball1, Ball ball2)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] Balls #{ball1.Id} and #{ball2.Id} collided.");
    }

    // Ball position update
    public void LogPosition(Ball ball, Vector2 newPosition)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] Ball #{ball.Id} NEW POS={newPosition.X},{newPosition.Y}");
    }

    // Ball velocity update
    public void LogVelocity(Ball ball, Vector2 newVelocity)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] Ball #{ball.Id} NEW VEL={newVelocity.X},{newVelocity.Y}");
    }

    // Wall collisions
    public void LogWallXCollision(Ball ball)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] Ball #{ball.Id} collided with a vertical wall at: {ball.Position.X},{ball.Position.Y}");
    }
    public void LogWallYCollision(Ball ball)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] Ball #{ball.Id} collided with a horizontal wall at: {ball.Position.X},{ball.Position.Y}");
    }

    private async Task BackgroundWrite()
    {
        await using var sw = new StreamWriter(_path, true, Encoding.ASCII);
        foreach (var entry in _queue.GetConsumingEnumerable())
        {
            await sw.WriteLineAsync(entry);
            await sw.FlushAsync();
        }
    }

    public void Dispose()
    {
        _queue.CompleteAdding();
        _writerTask.Wait();
        _queue.Dispose();
    }
}