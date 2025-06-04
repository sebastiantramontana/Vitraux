using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Helpers;

internal static class IoCRegistration
{
    internal static IServiceCollection AddHelpers(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IDataUriConverter, DataUriConverter>()
            .AddSingleton<INotImplementedCaseGuard, NotImplementedCaseGuard>();
}
