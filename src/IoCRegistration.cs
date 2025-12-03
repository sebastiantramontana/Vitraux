using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution;
using Vitraux.Execution.Building;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Building.Implementations.ElementBuilders.Actions;

namespace Vitraux;

public static class IoCRegistration
{
    public static IVitrauxRegistrar AddVitraux(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddSingleton<IActionKeyGenerator, ActionKeyGenerator>()
            .AddSingleton<IViewModelRepository, ViewModelRepository>()
            .AddExecution()
            .AddHelpers()
            .AddJsCodeGeneration()
            .CreateVitrauxRegistrar();

    public static Task BuildVitraux(this IServiceProvider serviceProvider)
    {
        var builder = serviceProvider.GetRequiredService<IVitrauxBuilder>();
        return builder.Build();
    }

    private static VitrauxRegistrar CreateVitrauxRegistrar(this IServiceCollection serviceCollection)
        => new(serviceCollection, serviceCollection.CreateModelRegistrar());

    private static ViewModelRegistrar CreateModelRegistrar(this IServiceCollection serviceCollection)
        => new(serviceCollection);
}
