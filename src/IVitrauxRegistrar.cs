namespace Vitraux;

public interface IVitrauxRegistrar
{
    IModelRegistrar AddConfiguration(Func<VitrauxConfiguration> config);
    IModelRegistrar AddDefaultConfiguration();
}
