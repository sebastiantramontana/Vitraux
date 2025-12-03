namespace ChronometerPkg;

public interface IChronometerUpdater
{
    Task Update(ChronometerEntity chronometerEntity);
}
