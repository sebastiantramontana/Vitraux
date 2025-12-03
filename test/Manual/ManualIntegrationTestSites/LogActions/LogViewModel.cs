using Vitraux;

namespace LogActions;

public class LogViewModel(IViewUpdater<LogViewModel> viewUpdater)
{
    public string Log { get; set; } = string.Empty;

    public void AddLog()
        => Console.WriteLine($"Delegate Sync method called at {DateTime.Now:HH:mm:ss}");

    public void AddLog(double number1, double number2, string extratext)
        => Console.WriteLine($"Sync parameter binder called at {DateTime.Now:HH:mm:ss} with parameters {number1}, {number2}, '{extratext}'");

    public Task AddLogAsync()
    {
        Log += $"Delegate Async method called at {DateTime.Now.ToString("HH:mm:ss")}{Environment.NewLine}";
        return viewUpdater.Update();
    }

    public Task AddLogAsync(double number1, double number2, string extratext)
    {
        Log += $"Async parameter binder called at {DateTime.Now:HH:mm:ss} with parameters {number1}, {number2}, '{extratext}'{Environment.NewLine}";
        return viewUpdater.Update();
    }
}
