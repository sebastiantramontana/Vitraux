using Vitraux.Execution.JsInvokers;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.Execution.Building;

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IJsFullObjectNamesGenerator jsFullObjectNamesGenerator,
    IRootJsGenerator rootJsGenerator,
    IJsInitializeNonCachedViewFunctionsInvoker jsInitializeNonCachedViewFunctionsInvoker,
    IJsTryInitializeViewFunctionsFromCacheByVersionInvoker jsTryInitializeViewFunctionsFromCacheByVersionInvoker,
    IJsInitializeNewViewFunctionsToCacheByVersionInvoker jsinitializeNewViewFunctionsToCacheByVersionInvoker,
    IViewModelJsNamesMapper encodedSerializationDataMapper,
    IViewModelJsNamesCache<TViewModel> vmSerializationDataCache,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IViewModelUpdateFunctionBuilder
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public async Task Build()
    {
        var vmKey = GenerateViewModelKey();
        var behavior = modelConfiguration.ConfigurationBehavior;
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);
        var fullObjNames = jsFullObjectNamesGenerator.Generate(mappingData);

        StoreSerializationData(vmKey, fullObjNames);

        switch (behavior.VMUpdateFunctionCaching)
        {
            case VMUpdateFunctionNoCache:
                InvokeInitializationViewFunctionsNoCache(vmKey, fullObjNames, behavior.QueryElementStrategy);
                break;
            case VMUpdateFunctionCacheByVersion cacheByVersion:
                await InvokeInitializationViewFunctionsByVersion(vmKey, cacheByVersion.Version, fullObjNames, behavior.QueryElementStrategy);
                break;
            default:
                notImplementedCaseGuard.ThrowException(behavior.VMUpdateFunctionCaching);
                break;
        }
    }

    private void StoreSerializationData(string vmKey, FullObjectNames fullObjectNames)
    {
        vmSerializationDataCache.ViewModelKey = vmKey;
        vmSerializationDataCache.ViewModelJsNames = encodedSerializationDataMapper.MapFromFull(fullObjectNames);
    }

    private GeneratedJsCode GenerateJsCode(FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy)
        => rootJsGenerator.GenerateJs(fullObjectNames, queryElementStrategy);

    private async ValueTask InvokeInitializationViewFunctionsByVersion(string vmKey, string version, FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy)
    {
        if (!await jsTryInitializeViewFunctionsFromCacheByVersionInvoker.Invoke(vmKey, version))
        {
            var generatedJsCode = GenerateJsCode(fullObjectNames, queryElementStrategy);
            jsinitializeNewViewFunctionsToCacheByVersionInvoker.Invoke(vmKey, version, generatedJsCode.InitializeViewJs, generatedJsCode.UpdateViewJs);
        }
    }

    private void InvokeInitializationViewFunctionsNoCache(string vmKey, FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var generatedJsCode = GenerateJsCode(fullObjectNames, queryElementStrategy);
        jsInitializeNonCachedViewFunctionsInvoker.Invoke(vmKey, generatedJsCode.InitializeViewJs, generatedJsCode.UpdateViewJs);
    }

    private static string GenerateViewModelKey()
        => typeof(TViewModel).FullName!.Replace('.', '-');
}
