using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

namespace Vitraux.JsCodeGeneration.BuiltInCalling;

internal static class IoCRegistration
{
    internal static IServiceCollection AddBuiltInCalling(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddStoredElements()
            .AddUpdating();

    private static IServiceCollection AddStoredElements(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IFetchElementCall, FetchElementCall>()
            .AddSingleton<IGetElementByIdAsArrayCall, GetElementByIdAsArrayCall>()
            .AddSingleton<IGetElementsByQuerySelectorCall, GetElementsByQuerySelectorCall>()
            .AddSingleton<IGetFetchedElementCall, GetFetchedElementCall>()
            .AddSingleton<IGetStoredElementByIdAsArrayCall, GetStoredElementByIdAsArrayCall>()
            .AddSingleton<IGetStoredElementsByQuerySelectorCall, GetStoredElementsByQuerySelectorCall>()
            .AddSingleton<IGetStoredTemplateCall, GetStoredTemplateCall>()
            .AddSingleton<IGetTemplateCall, GetTemplateCall>();

    private static IServiceCollection AddUpdating(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IIsValueValidCall, IsValueValidCall>()
            .AddSingleton<ISetElementsAttributeCall, SetElementsAttributeCall>()
            .AddSingleton<ISetElementsContentCall, SetElementsContentCall>()
            .AddSingleton<ISetElementsHtmlCall, SetElementsHtmlCall>()
            .AddSingleton<IToChildQueryFunctionCall, ToChildQueryFunctionCall>()
            .AddSingleton<IUpdateChildElementsFunctionCall, UpdateChildElementsFunctionCall>()
            .AddSingleton<IUpdateCollectionByPopulatingElementsCall, UpdateCollectionByPopulatingElementsCall>()
            .AddSingleton<IUpdateTableCall, UpdateTableCall>()
            .AddSingleton<IUpdateValueByInsertingElementsCall, UpdateValueByInsertingElementsCall>()
            .AddSingleton<IExecuteUpdateViewFunctionCall, ExecuteUpdateViewFunctionCall>();
}
