using Vitraux.Execution.Building.JsViewUpdatingInvokation;
using Vitraux.Execution.Serialization;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.Execution.Building;

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IRootJsGenerator rootJsGenerator,
    IJsInitializationInvoker jsInitializationExecutor,
    IJsIsVersionedUpdateViewFunctionRebuildNeededInvoker jsIsVersionedUpdateViewFunctionRebuildNeededInvoker,
    IJsCreateUpdateFunctionInvoker jsCreateUpdateFunctionExecutor,
    ISerializationDataMapper serializationDataMapper,
    IViewModelSerializationDataCache<TViewModel> vmSerializationDataCache)
    : IViewModelUpdateFunctionBuilder
    where TModelConfiguration : class, IModelConfiguration<TViewModel>
{
    public async Task Build()
    {
        var mappingData = modelConfiguration.ConfigureMapping(modelMapper);
        var behavior = modelConfiguration.ConfigurationBehavior;
        var generatedJs = rootJsGenerator.GenerateJs(mappingData, behavior.QueryElementStrategy);
        var vmKey = GenerateViewModelKey();

        StoreSerializationData(vmKey, generatedJs.UpdateViewInfo.ViewModelSerializationData);

        await jsInitializationExecutor.Execute(generatedJs.InitializeViewJs);
        await CreateUpdateFunction(behavior.VMUpdateFunctionCaching, vmKey, generatedJs.UpdateViewInfo.JsCode);
    }

    private void StoreSerializationData(string vmKey, ViewModelSerializationData serializationData)
    {
        vmSerializationDataCache.ViewModelKey = vmKey;
        vmSerializationDataCache.ViewModelSerializationData = serializationDataMapper.MapToEncoded(serializationData);
    }

    private ValueTask CreateUpdateFunction(VMUpdateFunctionCaching vmUpdateFunctionCaching, string vmKey, string jsCode)
        => vmUpdateFunctionCaching switch
        {
            VMUpdateFunctionNoCache => CreateUpdateFunctionNoCache(vmKey, jsCode),
            VMUpdateFunctionCacheByVersion cacheVersion => TryCreateUpdateFunctionVersionCached(vmKey, cacheVersion.Version, jsCode),
            _ => throw new NotSupportedException($"Unsupported VMUpdateFunctionCaching: {vmUpdateFunctionCaching}")
        };

    private async ValueTask TryCreateUpdateFunctionVersionCached(string vmKey, string version, string jsCode)
    {
        var isRebuildNeeded = await jsIsVersionedUpdateViewFunctionRebuildNeededInvoker.IsRebuildNeeded(vmKey, version);
        if (isRebuildNeeded)
        {
            await jsCreateUpdateFunctionExecutor.InvokeVersionCached(vmKey, version, jsCode);
        }
    }

    private ValueTask CreateUpdateFunctionNoCache(string vmKey, string jsCode)
        => jsCreateUpdateFunctionExecutor.InvokeNoCache(vmKey, jsCode);

    private static string GenerateViewModelKey()
        => typeof(TViewModel).FullName!.Replace('.', '-');
}
