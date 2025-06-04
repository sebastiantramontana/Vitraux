using Vitraux.Execution.Building.JsViewUpdatingInvokation;
using Vitraux.JsCodeGeneration;

namespace Vitraux.Execution.Building;

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IRootJsGenerator rootJsGenerator,
    IJsInitializationInvoker jsInitializationExecutor,
    IJsCreateUpdateFunctionInvoker jsCreateUpdateFunctionExecutor)
    : IViewModelUpdateFunctionBuilder
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public async Task Build()
    {
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);
        var behavior = modelConfiguration.ConfigurationBehavior;

        var generatedJs = rootJsGenerator.GenerateJs(mappingData, behavior.QueryElementStrategy);

        await jsInitializationExecutor.Execute(generatedJs.InitializeViewJs);

        switch (behavior.VMUpdateFunctionCaching)
        {
            case VMUpdateFunctionCacheByVersion cacheVersion:
                await jsCreateUpdateFunctionExecutor.ExecuteVersionCached(GenerateVMKey(), cacheVersion.Version, generatedJs.UpdateViewJs);
                break;
            case VMUpdateFunctionNoCache:
                await jsCreateUpdateFunctionExecutor.ExecuteNoCache(GenerateVMKey(), generatedJs.UpdateViewJs);
                break;
            default:
                throw new NotSupportedException($"Unsupported VMUpdateFunctionCaching: {behavior.VMUpdateFunctionCaching}");
        }
    }

    private static string GenerateVMKey()
        => typeof(TViewModel).FullName!.Replace('.', '-');
}
