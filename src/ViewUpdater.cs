using Vitraux.Execution.Building;

namespace Vitraux;

internal class ViewUpdater<TViewModel>(IObjectNamesRepository<TViewModel> objectNamesRepository) : IViewlUpdater<TViewModel>
{
    public void Update(TViewModel viewModel)
    {
        var viewModelSerializationData = objectNamesRepository.ViewModelSerializationData;
    }
}