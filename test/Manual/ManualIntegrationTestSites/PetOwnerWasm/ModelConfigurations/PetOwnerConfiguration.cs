using PetOwnerWasm.ViewModel;
using Vitraux;
using Vitraux.Helpers;

namespace PetOwnerWasm.ModelConfigurations;

public class PetOwnerConfiguration(IDataUriConverter dataUriConverter) : IModelConfiguration<PetOwner>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceAtStart,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("test 1.0")
    };

    public ModelMappingData ConfigureMapping(IModelMapper<PetOwner> modelMapper)
        => modelMapper
            .MapValue(po => po.Name).ToElements.ById("petowner-name").ToContent
            .MapValue(po => po.Address).ToElements.ById("petowner-address").ToContent
            .MapValue(po => po.PhoneNumber).ToElements.ById("petowner-phone-number").ToContent
            .MapValue(po => po.Subscription).ToOwnMapping
            .MapCollection(po => po.Pets)
                .ToTables.ById("petowner-pets")
                .PopulatingRows.FromTemplate("petowner-pet-row")
                    .MapValue(pet => pet.Name).ToElements.ByQuery("[data-id='pet-name']").ToContent
                    .MapCollection(pet => pet.Vaccines)
                        .ToContainerElements.ByQuery(".vaccines-list")
                        .FromTemplate("vaccine-item-template")
                        .ToOwnMapping
                    .EndCollection
                    .MapValue(pet => ToDataUri(pet.Photo)).ToElements.ByQuery("[data-id='pet-photo']").ToAttribute("src")
                    .MapCollection(pet => pet.Antiparasitics)
                        .ToContainerElements.ByQuery(".antiparasitics-list")
                        .FromTemplate("antiparasitic-item-template")
                            .MapValue(a => a.Name).ToElements.ByQuery(".antiparasitic-name").ToContent
                            .MapValue(a => a.DateApplied).ToElements.ByQuery(".antiparasitic-date").ToContent
                    .EndCollection
            .EndCollection
            .Data;

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
