using Vitraux.Execution.JsInvokers;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution;

internal class ViewUpdater<TViewModel>(
    IViewModelJsNamesCache<TViewModel> objectNamesRepository,
    IViewModelJsonSerializer jsonSerializer,
    IJsExecuteUpdateViewFunctionInvoker jsExecuteUpdateView) 
    : IViewlUpdater<TViewModel>
{
    public async Task Update(TViewModel viewModel)
    {
        if (viewModel is null)
        {
            return;
        }

        var viewModelSerializationData = objectNamesRepository.ViewModelSerializationData;
        var serializedViewModelJson = await jsonSerializer.Serialize(viewModelSerializationData, viewModel);

        await jsExecuteUpdateView.Invoke(objectNamesRepository.ViewModelKey, serializedViewModelJson);
    }
}