using Microsoft.Extensions.DependencyInjection;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Actions;

internal static class IoCRegistration
{
    internal static IServiceCollection AddActions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IGetElementsAttributeCall, GetElementsAttributeCall>()
            .AddSingleton<IGetElementsContentCall, GetElementsContentCall>()
            .AddSingleton<IGetInputValueCall, GetInputValueCall>()
            .AddSingleton<IRegisterActionCallGenerator, RegisterActionCallGenerator>()
            .AddSingleton<IRegisterActionAsyncCall, RegisterActionAsyncCall>()
            .AddSingleton<IRegisterActionSyncCall, RegisterActionSyncCall>()
            .AddSingleton<IRegisterParametrizableActionCallGenerator, RegisterParametrizableActionCallGenerator>()
            .AddSingleton<IRegisterParametrizableActionAsyncCall, RegisterParametrizableActionAsyncCall>()
            .AddSingleton<IRegisterParametrizableActionSyncCall, RegisterParametrizableActionSyncCall>();
}
