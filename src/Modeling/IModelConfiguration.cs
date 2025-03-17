using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling
{
    public interface IModelConfiguration<TViewModel>
    {
        public ModelMappingData ConfigureMapping(IModelMapperRoot<TViewModel> modelMapper);

        public ConfigurationBehavior ConfigurationBehavior { get; }
    }
}
