using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal static class IoCRegistration
{
    internal static IServiceCollection AddOnlyOnceAtStart(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IQueryElementsDeclaringOnlyOnceAtStartJsGenerator, QueryElementsDeclaringOnlyOnceAtStartJsGenerator>()
            .AddSingleton<IQueryElementsOnlyOnceAtStartJsGenerator, QueryElementsOnlyOnceAtStartJsGenerator>()
            .AddElementsStorage();
}