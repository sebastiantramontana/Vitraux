using Vitraux;

namespace Chronometer;

public class ChronometerUpdater(IServiceProvider serviceProvider, IViewUpdater<ChronometerViewModel> viewUpdater) : IChronometerUpdater
{
    public Task Update(ChronometerEntity chronometerEntity)
    {
        var viewModel = serviceProvider.GetRequiredService<ChronometerViewModel>();
        viewModel.Update(chronometerEntity);
        return viewUpdater.Update();
    }
}