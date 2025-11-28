using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution.ViewModelNames.Actions;

namespace Vitraux.Execution.ViewModelNames;

internal static class IoCRegistration
{
    internal static IServiceCollection AddViewModelNames(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IViewModelJsNamesMapper, ViewModelJsNamesMapper>()
            .AddSingleton<IViewModelKeyGenerator, ViewModelKeyGenerator>()
            .AddSingleton<IViewModelJsActionsRepository, ViewModelJsActionsRepository>();
}