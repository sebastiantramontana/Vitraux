using Microsoft.Extensions.DependencyInjection;

namespace Vitraux;

internal class VitrauxRegistrar(IServiceCollection serviceCollection, ModelRegistrar modelRegistrar) : IVitrauxRegistrar
{
    public IModelRegistrar AddConfiguration(Func<VitrauxConfiguration> config)
        => AddVitrauxConfiguration(config.Invoke());

    public IModelRegistrar AddDefaultConfiguration()
        => AddVitrauxConfiguration(VitrauxConfiguration.Default);

    private ModelRegistrar AddVitrauxConfiguration(VitrauxConfiguration vitrauxConfiguration)
    {
        _ = serviceCollection.AddSingleton(vitrauxConfiguration);
        return modelRegistrar;
    }
}
