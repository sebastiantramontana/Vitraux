using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

internal static class IoCRegistration
{
    internal static IServiceCollection AddOnlyOnceAtStart(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IQueryElementsDeclaringOnlyOnceAtStartJsGenerator, QueryElementsDeclaringOnlyOnceAtStartJsGenerator>()
            .AddSingleton<IQueryElementsOnlyOnceAtStartJsGenerator, QueryElementsOnlyOnceAtStartJsGenerator>()

        SEGUIR ACA

}