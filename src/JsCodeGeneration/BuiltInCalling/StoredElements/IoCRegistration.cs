using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;

internal static class IoCRegistration
{
    internal static IServiceCollection AddStoredElements(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IFetchElementCall, FetchElementCall>()
            .AddSingleton<IGetElementByIdAsArrayCall, GetElementByIdAsArrayCall>()
            .AddSingleton<IGetElementsByQuerySelectorCall, GetElementsByQuerySelectorCall>()
            .AddSingleton<IGetFetchedElementCall, GetFetchedElementCall>()
            .AddSingleton<IGetStoredElementByIdAsArrayCall, GetStoredElementByIdAsArrayCall>()
            .AddSingleton<IGetStoredElementsByQuerySelectorCall, GetStoredElementsByQuerySelectorCall>()
            .AddSingleton<IGetStoredTemplateCall, GetStoredTemplateCall>()
            .AddSingleton<IGetTemplateCall, GetTemplateCall>();
}
