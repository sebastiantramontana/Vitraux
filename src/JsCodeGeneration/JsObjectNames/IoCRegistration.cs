using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal static class IoCRegistration
{
    internal static IServiceCollection AddJsObjectNames(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsFullObjectNamesGenerator, JsFullObjectNamesGenerator>()
            .AddSingleton<IUniqueSelectorsFilter, UniqueSelectorsFilter>()
            .AddSingleton<IJsActionElementObjectNamesGenerator, JsActionElementObjectNamesGenerator>()
            .AddSingleton<IJsElementObjectNamesGenerator, JsElementObjectNamesGenerator>()
            .AddSingleton<IJsObjectNamesGenerator, JsObjectNamesGenerator>();
}
