using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal static class IoCRegistration
{
    internal static IServiceCollection AddOnlyOnceOnDemand(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsQueryElementsOnlyOnceOnDemandGeneratorContext, JsQueryElementsOnlyOnceOnDemandGeneratorContext>()
            .AddSingleton<IOnlyOnceOnDemandInitializeJsGenerator, OnlyOnceOnDemandInitializeJsGenerator>()
            .AddSingleton<IQueryElementsOnlyOnceOnDemandJsCodeGenerator, QueryElementsOnlyOnceOnDemandJsCodeGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator, QueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator, QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator, QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator, QueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator, QueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator, QueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator, QueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator>();
}

