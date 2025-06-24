using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.ViewModelNames;

internal static class IoCRegistration
{
    internal static IServiceCollection AddViewModelNames(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IViewModelJsNamesMapper, ViewModelJsNamesMapper>();
}