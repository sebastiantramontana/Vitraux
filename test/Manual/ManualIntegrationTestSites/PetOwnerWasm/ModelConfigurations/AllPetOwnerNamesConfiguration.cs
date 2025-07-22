using PetOwnerWasm.ViewModel;
using Vitraux;

namespace PetOwnerWasm.ModelConfigurations;

public class AllPetOwnerNamesConfiguration : IModelConfiguration<AllPetOwnerNames>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("1.0")
    };

    public ModelMappingData ConfigureMapping(IModelMapper<AllPetOwnerNames> modelMapper)
        => modelMapper
            .MapCollection(po => po.Names)
                .ToContainerElements.ById("petowners")
                .FromTemplate("petowner-option-template")
                    .MapValue(name => name.Id).ToElements.ByQuery("option").ToAttribute("value")
                    .MapValue(name => name.Name).ToElements.ByQuery("option").ToContent
            .EndCollection
            .Data;
}
