using Microsoft.Extensions.DependencyInjection;

namespace Vitraux.Execution.Serialization;

internal static class IoCRegistration
{
    internal static IServiceCollection AddSerialization(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IViewModelJsonSerializer, ViewModelJsonSerializer>()
            .AddSingleton<ISerializablePropertyValueExtractor, SerializablePropertyValueExtractor>();
}