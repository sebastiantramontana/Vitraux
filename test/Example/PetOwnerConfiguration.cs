using Vitraux.Helpers;

namespace Vitraux.Test.Example;

public class PetOwnerConfiguration(IDataUriConverter dataUriConverter) : IModelConfiguration<PetOwner>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new(QueryElementStrategy.OnlyOnceAtStart, true, true);

    public ModelMappingData ConfigureMapping(IModelMapper<PetOwner> modelMapper)
        => modelMapper
            .MapValue(po => po.Name)
                .ToElements.ById("petowner-name").ToContent
                .ToJsFunction("poNameFunction").FromModule(new Uri("./modules/po.js", UriKind.Relative))
            .MapValue(po => po.Address)
                .ToElements.ById("petowner-address-parent")
                    .Insert.FromTemplate("petowner-address-template")
                    .ToChildren.ByQuery(".petowner-address-target")
                        .ToContent
                .ToElements.ByQuery(".petwoner-address > div").ToAttribute("data-petowner-address")
            .MapValue(po => po.PhoneNumber)
                .ToElements.ByQuery(".petowner-phonenumber")
                    .Insert.FromUri(new Uri("./htmlpieces/phoneblock", UriKind.Relative))
                    .ToChildren.ByQuery(".petowner-phonenumber-target")
                        .ToAttribute("data-phonenumber")
                .ToElements.ById("petowner-phonenumber-id").ToContent
            .MapValue(po => po.Subscription)
                .ToOwnMapping
            .MapCollection(po => po.Pets)
                .ToJsFunction("pets.manage").FromModule(new Uri("./modules/pets.js", UriKind.Relative))
                .ToTables.ById("pets-table")
                .PopulatingRows.FromTemplate("pet-row-template")
                    .MapValue(pet => pet.Name)
                        .ToElements.ByQuery(".cell-pet-name").ToContent
                        .ToElements.ByQuery(".anchor-cell-pet-name").ToAttribute("href")
                        .ToElements.ByQuery(".another-anchor-cell-pet-name").ToAttribute("href")
                    .MapCollection(pet => pet.Vaccines)
                        .ToTables.ById("inner-table-vaccines")
                        .PopulatingRows.FromUri(new Uri("./htmlpieces/row-vaccines.html", UriKind.Relative))
                            .MapValue(v => v.Name).ToElements.ByQuery(".div-vaccine").ToContent
                            .MapValue(v => v.DateApplied).ToElements.ByQuery(".span-vaccine-date").ToContent
                            .MapCollection(v => v.Ingredients)
                                .ToContainerElements.ByQuery("> ingredients-list")
                                .FromTemplate("ingredients-template")
                                    .MapValue(i => i).ToElements.ByQuery("ingredient-item").ToContent
                            .EndCollection
                    .EndCollection
                    .MapValue(pet => ToDataUri(pet.Photo)).ToElements.ByQuery(".pet-photo").ToAttribute("src")
                    .MapCollection(pet => pet.Antiparasitics)
                        .ToJsFunction("globalThis.manageAntiparasitics")
                        .ToContainerElements.ByQuery("inner-nav-antiparasitics")
                        .FromUri(new Uri("./htmlpieces/row-antiparasitics.html", UriKind.Relative))
                            .MapValue(a => a.Name).ToElements.ByQuery(".div-antiparasitics").ToContent
                            .MapValue(a => a.DateApplied).ToElements.ByQuery(".span-antiparasitics-date").ToContent
                    .EndCollection
            .EndCollection
            .Data;

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
