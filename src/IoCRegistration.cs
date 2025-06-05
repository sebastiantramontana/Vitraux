using Microsoft.Extensions.DependencyInjection;
using Vitraux.Execution;
using Vitraux.Execution.Building;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;

namespace Vitraux;

public static class IoCRegistration
{
    public static IModelRegistrar AddVitraux(this IServiceCollection serviceCollection)
        => serviceCollection
            .AddExecution()
            .AddHelpers()
            .AddJsCodeGeneration()
            .CreateModelRegistrar();

    public static Task BuildVitraux(this IServiceProvider serviceProvider)
    {
        var builder = serviceProvider.GetRequiredService<IVitrauxBuilder>();
        return builder.Build();
    }

    private static ModelRegistrar CreateModelRegistrar(this IServiceCollection serviceCollection)
        => new(serviceCollection);
}
