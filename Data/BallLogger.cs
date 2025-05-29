using System.Collections.Concurrent;
using System.Text;

namespace Data;

internal class BallLogger
{
    private readonly BlockingCollection<string> _queue = new();
    private readonly Task _writerTask;
    private readonly string _path;

    public BallLogger(string path)
    {
        this._path = path;
        // File.WriteAllText(path, string.Empty, Encoding.ASCII);
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

    public async Task ShutdownAsync()
    {
        _queue.CompleteAdding();
        await _writerTask;
    }
}