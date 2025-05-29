using System.Collections.Concurrent;
using System.Text;
using System.IO;
using System;

namespace Data;

internal class BallLogger
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

    public void Log(string line, int ballId)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        _queue.Add($"[{timestamp}] Ball #{ballId}: {line}");
    }

    private async Task BackgroundWrite()
    {
        await using var sw = new StreamWriter(_path, true, Encoding.ASCII);
        foreach (var entry in _queue.GetConsumingEnumerable())
        {
            await sw.WriteLineAsync(entry);
        }
    }
}