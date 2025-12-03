namespace ChronometerPkg;

public class ChronometerViewModel(IChronometerService chronometerService)
{
    public int Hours { get; private set; }
    public int Minutes { get; private set; }
    public int Seconds { get; private set; }
    public int TenthSeconds { get; private set; }
    public bool IsRunning { get; private set; } = false;

    public void Update(ChronometerEntity chronometerEntity)
    {
        IsRunning = chronometerEntity.IsRunning;
        Hours = chronometerEntity.Hours;
        Minutes = chronometerEntity.Minutes;
        Seconds = chronometerEntity.Seconds;
        TenthSeconds = chronometerEntity.TenthSeconds;
    }

    public Task StartPause() => IsRunning ? Pause() : Start();
    public Task Reset() => chronometerService.Reset();
    private Task Start() => chronometerService.Start();
    private Task Pause() => chronometerService.Pause();
}
