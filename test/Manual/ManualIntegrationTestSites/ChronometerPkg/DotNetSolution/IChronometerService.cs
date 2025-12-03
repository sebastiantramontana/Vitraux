namespace ChronometerPkg;

public interface IChronometerService
{
    Task Start();
    Task Pause();
    Task Reset();
}
