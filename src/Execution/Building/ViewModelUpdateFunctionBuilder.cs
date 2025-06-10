using Vitraux.Execution.Building.JsViewUpdatingInvokation;
using Vitraux.JsCodeGeneration;

namespace Vitraux.Execution.Building;

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IRootJsGenerator rootJsGenerator,
    IJsInitializationInvoker jsInitializationExecutor,
    IJsCreateUpdateFunctionInvoker jsCreateUpdateFunctionExecutor,
    IObjectNamesRepository<TViewModel> objectNamesRepository)
    : IViewModelUpdateFunctionBuilder
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public async Task Build()
    {
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);
        var behavior = modelConfiguration.ConfigurationBehavior;

        var generatedJs = rootJsGenerator.GenerateJs(mappingData, behavior.QueryElementStrategy);

        await jsInitializationExecutor.Execute(generatedJs.InitializeViewJs);

        var vmKey = GenerateVMKey();

        switch (behavior.VMUpdateFunctionCaching)
        {
            case VMUpdateFunctionCacheByVersion cacheVersion:
                await jsCreateUpdateFunctionExecutor.ExecuteVersionCached(vmKey, cacheVersion.Version, generatedJs.UpdateViewInfo.JsCode);
                break;
            case VMUpdateFunctionNoCache:
                await jsCreateUpdateFunctionExecutor.ExecuteNoCache(vmKey, generatedJs.UpdateViewInfo.JsCode);
                break;
            default:
                throw new NotSupportedException($"Unsupported VMUpdateFunctionCaching: {behavior.VMUpdateFunctionCaching}");
        }

        objectNamesRepository.ViewModelSerializationData = generatedJs.UpdateViewInfo.ViewModelSerializationData;  //new ObjectNamesWithData(generatedJs.ValueObjects, generatedJs.CollectionObjects);
    }

    private static string GenerateVMKey()
        => typeof(TViewModel).FullName!.Replace('.', '-');
}
