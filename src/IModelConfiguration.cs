namespace Vitraux;

public interface IModelConfiguration<TViewModel>
{
    public ModelMappingData ConfigureMapping(IModelMapper<TViewModel> modelMapper);

    public ConfigurationBehavior ConfigurationBehavior { get; }
}
