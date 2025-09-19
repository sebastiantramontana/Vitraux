using Vitraux.Execution.JsInvokers;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.Execution.Building;

internal interface IViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    Task Build(string vmKey, ConfigurationBehavior configurationBehavior, FullObjectNames fullObjNames);
}

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IRootJsGenerator rootJsGenerator,
    IJsInitializeNonCachedViewFunctionsInvoker jsInitializeNonCachedViewFunctionsInvoker,
    IJsTryInitializeViewFunctionsFromCacheByVersionInvoker jsTryInitializeViewFunctionsFromCacheByVersionInvoker,
    IJsInitializeNewViewFunctionsToCacheByVersionInvoker jsinitializeNewViewFunctionsToCacheByVersionInvoker,
    IViewModelJsNamesMapper encodedSerializationDataMapper,
    IViewModelJsNamesCacheGeneric<TViewModel> vmSerializationDataCache,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public async Task Build(string vmKey, ConfigurationBehavior configurationBehavior, FullObjectNames fullObjNames)
    {
        StoreSerializationData(vmKey, fullObjNames);

        switch (configurationBehavior.VMUpdateFunctionCaching)
        {
            case VMUpdateFunctionNoCache:
                await InvokeInitializationViewFunctionsNoCache(vmKey, fullObjNames, configurationBehavior.QueryElementStrategy);
                break;
            case VMUpdateFunctionCacheByVersion cacheByVersion:
                await InvokeInitializationViewFunctionsByVersion(vmKey, cacheByVersion.Version, fullObjNames, configurationBehavior.QueryElementStrategy);
                break;
            default:
                notImplementedCaseGuard.ThrowException(configurationBehavior.VMUpdateFunctionCaching);
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
            await jsinitializeNewViewFunctionsToCacheByVersionInvoker.Invoke(vmKey, version, generatedJsCode.InitializeViewJs, generatedJsCode.UpdateViewJs);
        }
    }

    private ValueTask InvokeInitializationViewFunctionsNoCache(string vmKey, FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var generatedJsCode = GenerateJsCode(fullObjectNames, queryElementStrategy);
        return jsInitializeNonCachedViewFunctionsInvoker.Invoke(vmKey, generatedJsCode.InitializeViewJs, generatedJsCode.UpdateViewJs);
    }
}
