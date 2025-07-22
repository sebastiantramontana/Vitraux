using PetOwnerWasm.ViewModel;
using Vitraux;

namespace PetOwnerWasm.ModelConfigurations;

internal class VaccineConfiguration : IModelConfiguration<Vaccine>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.NoCache
    };

    public ModelMappingData ConfigureMapping(IModelMapper<Vaccine> modelMapper)
        => modelMapper
            .MapValue(v => v.Name).ToElements.ByQuery(".vaccine-name").ToContent
            .MapValue(v => v.DateApplied).ToElements.ByQuery(".vaccine-date").ToContent
            .MapCollection(v => v.Ingredients)
                .ToContainerElements.ByQuery(".ingredients-list")
                .FromTemplate("vaccine-ingredient-template")
                .MapValue(i => i).ToElements.ByQuery(".ingredient-item").ToContent
            .EndCollection
            .Data;
}
