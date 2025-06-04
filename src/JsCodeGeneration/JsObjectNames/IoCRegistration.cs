using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal static class IoCRegistration
{
    internal static IServiceCollection AddJsObjectNames(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IJsObjectNamesGenerator, JsObjectNamesGenerator>()
            .AddSingleton<IUniqueSelectorsFilter, UniqueSelectorsFilter>();
}
