using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.Actions;

internal static class IoCRegistration
{
    internal static IServiceCollection AddActions(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IViewModelActionFunctionInvokerContext, ViewModelActionFunctionInvokerContext>()
            .AddSingleton<IViewModelActionFunctionInvokerStrategy, AlwaysViewModelActionFunctionInvokerStrategy>()
            .AddSingleton<IViewModelActionFunctionInvokerStrategy, OnFirstViewModelActionFunctionInvokerStrategy>()
            .AddSingleton<IViewModelActionFunctionInvokerStrategy, OnlyOnceAtStartViewModelActionFunctionInvokerStrategy>();
}