using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Building;

internal class ViewModelRuntimeBuilder<TViewModel>(
    IViewModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IViewModelKeyGenerator viewModelKeyGenerator,
    IViewModelUpdateFunctionBuilder<TViewModel> viewModelUpdateFunctionBuilder,
    IViewModelActionsBuilder<TViewModel> viewModelActionsBuilder) : IBuilder
{
    public Task Build()
    {
        var vmKey = viewModelKeyGenerator.Generate<TViewModel>();
        var behavior = modelConfiguration.ConfigurationBehavior;
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);

        var updateFuncTask = viewModelUpdateFunctionBuilder.Build(vmKey, behavior, mappingData);
        var actionsTask = viewModelActionsBuilder.Build(vmKey, behavior, mappingData.Actions);

        return Task.WhenAll(updateFuncTask, actionsTask);
    }
}
