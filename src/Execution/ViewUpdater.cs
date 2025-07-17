using Vitraux.Execution.JsInvokers;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution;

internal class ViewUpdater<TViewModel>(
    IViewModelJsNamesCacheGeneric<TViewModel> objectNamesRepository,
    IModelConfiguration<TViewModel> modelConfiguration,
    IViewModelChangeTrackingContext<TViewModel> viewModelChangeTrackingContext,
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

        var viewModelChangesTracker = viewModelChangeTrackingContext.GetChangesTracker(modelConfiguration.ConfigurationBehavior.TrackChanges);
        var trackedViewModelData = viewModelChangesTracker.Track(viewModel, objectNamesRepository.ViewModelJsNames);
        var serializedViewModelJson = await jsonSerializer.Serialize(trackedViewModelData);

        await jsExecuteUpdateView.Invoke(objectNamesRepository.ViewModelKey, serializedViewModelJson);
    }
}