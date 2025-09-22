using Vitraux.Execution.ViewModelNames;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Execution.Building;

internal class ViewModelRuntimeBuilder<TViewModel, TModelConfiguration>(
    IJsFullObjectNamesGenerator jsFullObjectNamesGenerator,
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IViewModelKeyGenerator viewModelKeyGenerator,
    IViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration> viewModelUpdateFunctionBuilder,
    IViewModelActionsBuilder<TViewModel> viewModelActionsBuilder) : IBuilder
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public Task Build()
    {
        var vmKey = viewModelKeyGenerator.Generate<TViewModel>();
        var behavior = modelConfiguration.ConfigurationBehavior;
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);
        var fullObjNames = jsFullObjectNamesGenerator.Generate(mappingData);

        var updateFuncTask = viewModelUpdateFunctionBuilder.Build(vmKey, behavior, fullObjNames);
        var actionsTask = viewModelActionsBuilder.Build(vmKey, behavior, mappingData.Actions, fullObjNames.JsElementObjectNames);

        return Task.WhenAll(updateFuncTask, actionsTask);
    }
}

internal interface IViewModelActionsBuilder<TViewModel>
{
    public Task Build(string vmKey, ConfigurationBehavior configurationBehavior, IEnumerable<ActionData> Actions, IEnumerable<JsElementObjectName> JsElementObjectNames);
}

internal class ViewModelActionsBuilder<TViewModel> : IViewModelActionsBuilder<TViewModel>
{
    public Task Build(string vmKey, ConfigurationBehavior configurationBehavior, IEnumerable<ActionData> Actions, IEnumerable<JsElementObjectName> JsElementObjectNames)
    {
        throw new NotImplementedException();
    }
}
