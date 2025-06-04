using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal static class IoCRegistration
{
    internal static IServiceCollection AddAlways(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IAlwaysInitializeJsGenerator, AlwaysInitializeJsGenerator>()
            .AddSingleton<IJsQueryElementsDeclaringAlwaysGeneratorContext, JsQueryElementsDeclaringAlwaysGeneratorContext>()
            .AddSingleton<IQueryElementsAlwaysJsCodeGenerator, QueryElementsAlwaysJsCodeGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysByIdJsGenerator, QueryElementsDeclaringAlwaysByIdJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysByQuerySelectorJsGenerator, QueryElementsDeclaringAlwaysByQuerySelectorJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysCodeGenerator, QueryElementsDeclaringAlwaysCodeGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator, QueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysCollectionByUriJsGenerator, QueryElementsDeclaringAlwaysCollectionByUriJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysValueByTemplateJsGenerator, QueryElementsDeclaringAlwaysValueByTemplateJsGenerator>()
            .AddSingleton<IQueryElementsDeclaringAlwaysValueByUriJsGenerator, QueryElementsDeclaringAlwaysValueByUriJsGenerator>();
}
