using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal static class IoCRegistration
{
    internal static IServiceCollection AddValue(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IStorageElementJsLineGeneratorElementsById, StorageElementJsLineGeneratorElementsById>()
            .AddSingleton<IStorageElementJsLineGeneratorElementsByQuery, StorageElementJsLineGeneratorElementsByQuery>()
            .AddSingleton<IStorageElementJsLineGeneratorInsertElementsByTemplate, StorageElementJsLineGeneratorInsertElementsByTemplate>()
            .AddSingleton<IStorageElementJsLineGeneratorInsertElementsByUri, StorageElementJsLineGeneratorInsertElementsByUri>()
            .AddSingleton<IStorageElementValueJsLineGenerator, StorageElementValueJsLineGenerator>();
}
