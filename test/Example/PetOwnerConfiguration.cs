using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Test.Example;

public class PetOwnerConfiguration(IDataUriConverter dataUriConverter) : IModelConfiguration<PetOwner>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new(QueryElementStrategy.OnlyOnceAtStart, true);

    public ModelMappingData ConfigureMapping(IModelMapper<PetOwner> modelMapper)
        => modelMapper
            .MapValue(po => po.Name)
                .ToElements.ById("petowner-name").ToContent
            .MapValue(po => po.Address)
                .ToElements.ById("petowner-parent")
                    .Insert.FromTemplate("petowner-address-template")
                    .ToChildren.ByQuery(".petowner-address-target")
                    .ToContent
                .ToElements.ByQuery(".petwoner-address > div").ToAttribute("data-petowner-address")
            .MapValue(po => po.PhoneNumber)
                .ToElements.ByQuery(".petowner-phonenumber")
                    .Insert.FromUri(new Uri("https://mysite.com/htmlparts/phoneblock"))
                    .ToChildren.ByQuery(".petowner-phonenumber-target")
                    .ToAttribute("data-phonenumber")
                .ToElements.ById("petowner-phonenumber-id").ToContent
            .MapCollection(po => po.Pets)
                .ToTables.ById("pets-table")
                .PopulatingRows.FromTemplate("pet-row-template")
                    .MapValue(pet => pet.Name)
                        .ToElements.ByQuery(".cell-pet-name").ToContent
                        .ToElements.ByQuery(".anchor-cell-pet-name").ToAttribute("href")
                        .ToElements.ByQuery(".another-anchor-cell-pet-name").ToAttribute("href")
                    .MapCollection(pet => pet.Vaccines)
                        .ToTable.ById("inner-table-vaccines")
                        .ByPopulatingRows.FromFetch(new Uri("http://mysite.com/htmlparts/row-vaccines.html"))
                            .MapValue(v => v.Name).ToElements.ByQuery(".div-vaccine").ToContent
                            .MapValue(v => v.DateApplied).ToElements.ByQuery(".span-vaccine-date").ToContent
                    .EndCollection
                    .MapValue(pet => ToDataUri(pet.Photo)).ToElements.ByQuery(".pet-photo").ToAttribute("src")
                    .MapCollection(pet => pet.Antiparasitics)
                        .ByPopulatingElements.ByQuery("inner-nav-antiparasitics")
                        .FromFetch(new Uri("http://mysite.com/htmlparts/row-antiparasitics.html"))
                            .MapValue(a => a.Name).ToElements.ByQuery(".div-antiparasitics").ToContent
                            .MapValue(a => a.DateApplied).ToElements.ByQuery(".span-antiparasitics-date").ToContent
                    .EndCollection
            .EndCollection;

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
