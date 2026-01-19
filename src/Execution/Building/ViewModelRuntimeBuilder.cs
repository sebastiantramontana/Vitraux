using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Building;

internal class ViewModelRuntimeBuilder<TViewModel>(
    IViewModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IViewModelKeyGenerator viewModelKeyGenerator,
    IViewModelUpdateFunctionBuilder<TViewModel> viewModelUpdateFunctionBuilder,
    IViewModelActionsBuilder<TViewModel> viewModelActionsBuilder,
    IViewModelRepository viewModelRepository,
    IServiceProvider serviceProvider) : IBuilder where TViewModel : class
{
    public Task Build()
    {
        var vmKey = viewModelKeyGenerator.Generate<TViewModel>();
        var behavior = modelConfiguration.ConfigurationBehavior;
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);

        TrySaveViewModelInstance(vmKey, behavior);

        var updateFuncTask = viewModelUpdateFunctionBuilder.Build(vmKey, behavior, mappingData);
        var actionsTask = viewModelActionsBuilder.Build(vmKey, behavior, mappingData.Actions);

        return Task.WhenAll(updateFuncTask, actionsTask);
    }

    private void TrySaveViewModelInstance(string vmKey, ConfigurationBehavior behavior)
    {
        viewModelRepository.ConfigurationBehavior = behavior;

        var viewModel = serviceProvider.GetService<TViewModel>();

        if (viewModel is not null)
            viewModelRepository.SetViewModelInstance<TViewModel>(vmKey, viewModel);
    }
}
