using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Vitraux.JsCodeGeneration;

namespace Vitraux.Execution;

internal interface IJsInitializationExecutor
{
    ValueTask Execute(string jsCode);
}

internal class JsInitializationExecutor(IJSRuntime jSRuntime) : IJsInitializationExecutor
{
    private const string InitializationFunctionName = "globalThis.vitraux.updating.vmFunctions.executeInitializationView";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask Execute(string jsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(InitializationFunctionName, jsCode);
}

internal interface IJsCreateUpdateFunctionExecutor
{
    ValueTask ExecuteVersionCached(string vmKey, string version, string jsCode);
    ValueTask ExecuteNoCache(string vmKey, string jsCode);
}

internal class JsCreateUpdateFunctionExecutor(IJSRuntime jSRuntime) : IJsCreateUpdateFunctionExecutor
{
    private const string CreateUpdateVersionedFunctionName = "globalThis.vitraux.updating.vmFunctions.createVersionedUpdateViewFunction";
    private const string CreateUpdateNoCachedFunctionName = "globalThis.vitraux.updating.vmFunctions.createUpdateViewFunction";
    private readonly IJSInProcessRuntime _jsInProcessRuntime = (IJSInProcessRuntime)jSRuntime;

    public ValueTask ExecuteNoCache(string vmKey, string jsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(CreateUpdateNoCachedFunctionName, vmKey, jsCode);

    public ValueTask ExecuteVersionCached(string vmKey, string version, string jsCode)
        => _jsInProcessRuntime.InvokeVoidAsync(CreateUpdateVersionedFunctionName, vmKey, version, jsCode);
}

internal interface IModelRegistrar
{
    IModelRegistrar AddModelConfiguration<TViewModel, TModelConfiguration>() where TModelConfiguration : class, IModelConfiguration<TViewModel>;
}

internal class ModelRegistrar(ServiceCollection container) : IModelRegistrar
{
    public IModelRegistrar AddModelConfiguration<TViewModel, TModelConfiguration>()
        where TModelConfiguration : class, IModelConfiguration<TViewModel>
    {
        _ = container
            .AddSingleton<IModelMapper<TViewModel>, ModelMapper<TViewModel>>()
            .AddSingleton<IModelConfiguration<TViewModel>, TModelConfiguration>()
            .AddSingleton<IViewModelUpdateFunctionBuilder, ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>>();
        return this;
    }
}

internal interface IViewModelUpdateFunctionBuilder
{
    Task Build();
}

internal class ViewModelUpdateFunctionBuilder<TViewModel, TModelConfiguration>(
    IModelConfiguration<TViewModel> modelConfiguration,
    IModelMapper<TViewModel> modelMapper,
    IRootJsGenerator rootJsGenerator,
    IJsInitializationExecutor jsInitializationExecutor,
    IJsCreateUpdateFunctionExecutor jsCreateUpdateFunctionExecutor)
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

internal class VitrauxBuilder(IEnumerable<IViewModelUpdateFunctionBuilder> viewBuilders)
{
    public async Task Build()
    {
        var tasks = viewBuilders.Select(builder => builder.Build()).ToArray();

        await Task.WhenAll(tasks);
    }
}