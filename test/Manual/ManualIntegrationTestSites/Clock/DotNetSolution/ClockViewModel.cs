namespace ClockWasm;

public class ClockViewModel()
{
    private TimeSpan _clock = TimeSpan.Zero;

    public int Hours => _clock.Hours;
    public int Minutes => _clock.Minutes;
    public int Seconds => _clock.Seconds;
    public int TenthSeconds => _clock.Milliseconds / 100;
    public void Reset() => _clock = TimeSpan.Zero;
    public void AddTenthSecond() => _clock += TimeSpan.FromSeconds(1) / 10.0;
}
