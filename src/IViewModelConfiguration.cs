namespace Vitraux;

public interface IViewModelConfiguration<TViewModel>
{
    public ModelMappingData ConfigureMapping(IModelMapper<TViewModel> modelMapper);

    public ConfigurationBehavior ConfigurationBehavior { get; }
}
