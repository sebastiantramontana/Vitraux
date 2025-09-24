namespace Vitraux.Execution.Building;

internal interface IViewModelUpdateFunctionBuilder<TViewModel>
{
    Task Build(string vmKey, ConfigurationBehavior configurationBehavior, ModelMappingData modelMappingData);
}
