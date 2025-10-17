using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal static class IoCRegistration
{
    internal static IServiceCollection AddUpdating(this IServiceCollection serviceCollection)
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
