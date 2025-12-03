using Vitraux.Execution.Actions;
using Vitraux.Execution.JsInvokers.ViewFunctions;
using Vitraux.Execution.Serialization;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution;

internal class ViewUpdater<TViewModel>(
    IViewModelJsNamesRepositoryGeneric<TViewModel> objectNamesRepository,
    IViewModelConfiguration<TViewModel> modelConfiguration,
    IViewModelChangeTrackingContext<TViewModel> viewModelChangeTrackingContext,
    IViewModelJsonSerializer jsonSerializer,
    IJsExecuteUpdateViewFunctionInvoker jsExecuteUpdateView,
    IViewModelActionFunctionInvokerContext viewModelActionFunctionInvokerContext,
    IViewModelRepository viewModelRepository)
    : IViewUpdater<TViewModel> where TViewModel : class
{
    public Task Update(TViewModel viewModel)
    {
        viewModelRepository.SetViewModelInstance(objectNamesRepository.ViewModelKey, viewModel);
        return UpdateView(viewModel);
    }

    public Task Update()
    {
        var viewModel = viewModelRepository.GetViewModelInstance<TViewModel>(objectNamesRepository.ViewModelKey);
        return UpdateView(viewModel);
    }

    private async Task UpdateView(TViewModel viewModel)
    {
        var viewModelChangesTracker = viewModelChangeTrackingContext.GetChangesTracker(modelConfiguration.ConfigurationBehavior.TrackChanges);
        var trackedViewModelData = viewModelChangesTracker.Track(viewModel, objectNamesRepository.ViewModelJsNames);
        var serializedViewModelJson = await jsonSerializer.Serialize(trackedViewModelData);

        await jsExecuteUpdateView.Invoke(objectNamesRepository.ViewModelKey, serializedViewModelJson);

        viewModelActionFunctionInvokerContext
            .GetStrategy(modelConfiguration.ConfigurationBehavior.ActionRegistrationStrategy)
            .Invoke(objectNamesRepository.ViewModelKey);
    }
}
