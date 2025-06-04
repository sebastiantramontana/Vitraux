using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.Collections;

internal static class IoCRegistration
{
    internal static IServiceCollection AddCollections(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<ICollectionNamesGenerator, CollectionNamesGenerator>()
            .AddSingleton<ICollectionsJsGenerationBuilder, CollectionsJsGenerationBuilder>()
            .AddSingleton<ICollectionUpdateFunctionNameGenerator, CollectionUpdateFunctionNameGenerator>()
            .AddSingleton<IUpdateCollectionFunctionCallbackJsCodeGenerator, UpdateCollectionFunctionCallbackJsCodeGenerator>()
            .AddSingleton<IUpdateCollectionJsCodeGenerator, UpdateCollectionJsCodeGenerator>();
}
