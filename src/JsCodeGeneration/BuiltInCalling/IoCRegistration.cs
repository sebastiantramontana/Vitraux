using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.BuiltInCalling;

internal static class IoCRegistration
{
    internal static IServiceCollection AddBuiltInCalling(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddStoredElements()
            .AddUpdating()
            .AddActions();
}
