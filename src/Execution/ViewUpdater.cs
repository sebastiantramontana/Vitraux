using Vitraux.Execution.Actions;
using Vitraux.Execution.JsInvokers.ViewFunctions;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Execution.ViewModelNames.Actions;

namespace Vitraux.Execution;

internal class ViewUpdater<TViewModel>(
    IViewModelJsNamesRepositoryGeneric<TViewModel> objectNamesRepository,
    IModelConfiguration<TViewModel> modelConfiguration,
    IViewModelChangeTrackingContext<TViewModel> viewModelChangeTrackingContext,
    IViewModelJsonSerializer jsonSerializer,
    IJsExecuteUpdateViewFunctionInvoker jsExecuteUpdateView,
    IViewModelActionFunctionInvokerContext viewModelActionFunctionInvokerContext,
    IViewModelJsActionsRepository viewModelJsActionsRepository)
    : IViewUpdater<TViewModel> where TViewModel : notnull
{
    public async Task Update(TViewModel viewModel)
    {
        viewModelJsActionsRepository.SetViewModelInstance(objectNamesRepository.ViewModelKey, viewModel);

        var viewModelChangesTracker = viewModelChangeTrackingContext.GetChangesTracker(modelConfiguration.ConfigurationBehavior.TrackChanges);
        var trackedViewModelData = viewModelChangesTracker.Track(viewModel, objectNamesRepository.ViewModelJsNames);
        var serializedViewModelJson = await jsonSerializer.Serialize(trackedViewModelData);

        await jsExecuteUpdateView.Invoke(objectNamesRepository.ViewModelKey, serializedViewModelJson);

        viewModelActionFunctionInvokerContext
            .GetStrategy(modelConfiguration.ConfigurationBehavior.ActionRegistrationStrategy)
            .Invoke(objectNamesRepository.ViewModelKey);
    }
}
