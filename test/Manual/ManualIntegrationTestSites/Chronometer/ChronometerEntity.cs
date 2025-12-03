namespace Chronometer;

public class ChronometerEntity
{
    private TimeSpan _chronos = TimeSpan.Zero;

    public int Hours => _chronos.Hours;
    public int Minutes => _chronos.Minutes;
    public int Seconds => _chronos.Seconds;
    public int TenthSeconds => _chronos.Milliseconds / 100;
    public bool IsRunning { get; set; } = false;
    public void AddTime(TimeSpan time) => _chronos += time;
    public void Reset() => _chronos = TimeSpan.Zero;
}
