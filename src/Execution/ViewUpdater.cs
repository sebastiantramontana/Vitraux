using Vitraux.Execution.Serialization;

namespace Vitraux.Execution;

internal class ViewUpdater<TViewModel>(
    IViewModelSerializationDataCache<TViewModel> objectNamesRepository,
    IViewModelJsonSerializer jsonSerializer,
    IJsExecuteUpdateViewFunctionInvoker jsExecuteUpdateView) 
    : IViewlUpdater<TViewModel>
{
    public async Task Update(TViewModel viewModel)
    {
        if (viewModel is null)
            return;

        var viewModelSerializationData = objectNamesRepository.ViewModelSerializationData;
        var serializedViewModelJson = await jsonSerializer.Serialize(viewModelSerializationData, viewModel);

        await jsExecuteUpdateView.Invoke(objectNamesRepository.ViewModelKey, serializedViewModelJson);
    }
}