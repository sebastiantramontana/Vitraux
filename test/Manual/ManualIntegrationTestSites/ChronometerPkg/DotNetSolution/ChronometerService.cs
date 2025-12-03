namespace ChronometerPkg;

public class ChronometerService(IChronometerUpdater chronometerUpdater) : IChronometerService
{
    private CancellationTokenSource _cancellationTokenSouce = new();
    private readonly ChronometerEntity _chronometer = new();
    private readonly TimeSpan _tenthSeconds = TimeSpan.FromSeconds(1) / 10.0;

    public async Task Start()
    {
        _chronometer.IsRunning = true;

        using var timer = new PeriodicTimer(_tenthSeconds);

        try
        {
            while (await timer.WaitForNextTickAsync(_cancellationTokenSouce.Token))
            {
                _chronometer.AddTime(_tenthSeconds);
                await chronometerUpdater.Update(_chronometer);
            }
        }
        catch (OperationCanceledException)
        {
            if (!_cancellationTokenSouce.TryReset())
            {
                _cancellationTokenSouce.Dispose();
                _cancellationTokenSouce = new CancellationTokenSource();
            }
        }
    }

    public Task Pause()
    {
        _chronometer.IsRunning = false;
        _cancellationTokenSouce.Cancel();
        return chronometerUpdater.Update(_chronometer);
    }

    public Task Reset()
    {
        _chronometer.Reset();
        return chronometerUpdater.Update(_chronometer);
    }
}