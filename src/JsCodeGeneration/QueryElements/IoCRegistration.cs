using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal static class IoCRegistration
{
    internal static IServiceCollection AddQueryElements(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IQueryElementsJsCodeGeneratorContext, QueryElementsJsCodeGeneratorContext>()
            .AddSingleton<IQueryElementsJsGenerator, QueryElementsJsGenerator>()
            .AddSingleton<IJsElementObjectNamesGenerator, JsElementObjectNamesGenerator>()
            .AddStrategies();
}