using Vitraux.Execution.JsInvokers;
using Vitraux.Execution.Serialization;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.Execution.Building;

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IJsFullObjectNamesGenerator jsFullObjectNamesGenerator,
    IJsElementObjectNamesGenerator jsElementObjectNamesGenerator,
    IRootJsGenerator rootJsGenerator,
    IJsInitializeNonCachedViewFunctionsInvoker jsInitializeNonCachedViewFunctionsInvoker,
    IJsTryInitializeViewFunctionsFromCacheByVersionInvoker jsTryInitializeViewFunctionsFromCacheByVersionInvoker,
    IJsInitializeNewViewFunctionsToCacheByVersionInvoker jsinitializeNewViewFunctionsToCacheByVersionInvoker,
    IEncodedSerializationDataMapper encodedSerializationDataMapper,
    IViewModelSerializationDataCache<TViewModel> vmSerializationDataCache,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IViewModelUpdateFunctionBuilder
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public async Task Build()
    {
        var vmKey = GenerateViewModelKey();
        var behavior = modelConfiguration.ConfigurationBehavior;
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);

        var jsObjElementNames = jsElementObjectNamesGenerator.Generate(string.Empty, mappingData);
        var fullObjNames = jsFullObjectNamesGenerator.Generate(mappingData, jsObjElementNames);

        StoreSerializationData(vmKey, fullObjNames);

        switch (behavior.VMUpdateFunctionCaching)
        {
            case VMUpdateFunctionNoCache:
                InvokeInitializationViewFunctionsNoCache(vmKey, fullObjNames, jsObjElementNames, behavior.QueryElementStrategy);
                break;
            case VMUpdateFunctionCacheByVersion cacheByVersion:
                await InvokeInitializationViewFunctionsByVersion(vmKey, cacheByVersion.Version, fullObjNames, jsObjElementNames, behavior.QueryElementStrategy);
                break;
            default:
                notImplementedCaseGuard.ThrowException(behavior.VMUpdateFunctionCaching);
                break;
        }
    }

    private void StoreSerializationData(string vmKey, FullObjectNames fullObjectNames)
    {
        vmSerializationDataCache.ViewModelKey = vmKey;
        vmSerializationDataCache.ViewModelSerializationData = encodedSerializationDataMapper.MapToEncoded(fullObjectNames);
    }

    private GeneratedJsCode GenerateJsCode(FullObjectNames fullObjectNames, IEnumerable<JsObjectName> allJsElementObjectNames, QueryElementStrategy queryElementStrategy)
        => rootJsGenerator.GenerateJs(fullObjectNames, allJsElementObjectNames, queryElementStrategy);

    private async ValueTask InvokeInitializationViewFunctionsByVersion(string vmKey, string version, FullObjectNames fullObjectNames, IEnumerable<JsObjectName> allJsElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        if (!await jsTryInitializeViewFunctionsFromCacheByVersionInvoker.Invoke(vmKey, version))
        {
            var generatedJsCode = GenerateJsCode(fullObjectNames, allJsElementObjectNames, queryElementStrategy);
            jsinitializeNewViewFunctionsToCacheByVersionInvoker.Invoke(vmKey, version, generatedJsCode.InitializeViewJs, generatedJsCode.UpdateViewJs);
        }
    }

    private void InvokeInitializationViewFunctionsNoCache(string vmKey, FullObjectNames fullObjectNames, IEnumerable<JsObjectName> allJsElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var generatedJsCode = GenerateJsCode(fullObjectNames, allJsElementObjectNames, queryElementStrategy);
        jsInitializeNonCachedViewFunctionsInvoker.Invoke(vmKey, generatedJsCode.InitializeViewJs, generatedJsCode.UpdateViewJs);
    }

    private static string GenerateViewModelKey()
        => typeof(TViewModel).FullName!.Replace('.', '-');
}
