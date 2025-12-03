using Microsoft.Extensions.DependencyInjection;

namespace Vitraux;

internal class VitrauxRegistrar(IServiceCollection serviceCollection, IViewModelRegistrar modelRegistrar) : IVitrauxRegistrar
{
    public IViewModelRegistrar AddConfiguration(Func<VitrauxConfiguration> config)
        => AddVitrauxConfiguration(config.Invoke());

    public IViewModelRegistrar AddDefaultConfiguration()
        => AddVitrauxConfiguration(VitrauxConfiguration.Default);

    private IViewModelRegistrar AddVitrauxConfiguration(VitrauxConfiguration vitrauxConfiguration)
    {
        _ = serviceCollection.AddSingleton(vitrauxConfiguration);
        return modelRegistrar;
    }
}
