namespace Chronometer;

public interface IChronometerUpdater
{
    Task Update(ChronometerEntity chronometerEntity);
}
