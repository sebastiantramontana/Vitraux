namespace Vitraux;

public interface IVitrauxRegistrar
{
    IViewModelRegistrar AddConfiguration(Func<VitrauxConfiguration> config);
    IViewModelRegistrar AddDefaultConfiguration();
}
