using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Test.Example;

public class PetOwnerConfiguration(IDataUriConverter dataUriConverter) : IModelConfiguration<PetOwner>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new(QueryElementStrategy.OnlyOnceAtStart, true);

    public IModelMappingData ConfigureMapping(IModelMapperRoot<PetOwner> modelMapper)
        => modelMapper
            .MapValue(po => po.Name)
                .ToElements.ById("petowner-name")
                .ToContent
            .MapValue(po => po.Address)
                .ByPopulatingElements.ById("petowner-parent")
                    .FromTemplate("petowner-address-template")
                    .ToChildren.ByQuery(".petowner-address-target")
                    .ToContent
                .ToElements.ByQuery(".petwoner-address > div")
                    .ToAttribute("data-petowner-address")
            .MapCollection(po => po.pets)
                .ToTable.ById("pets-table")
                .ByPopulatingRows.FromTemplate("pet-row-template")
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
                        .ToTable.ByQuery("inner-table-antiparasitics")
                        .ByPopulatingRows.FromFetch(new Uri("http://mysite.com/htmlparts/row-antiparasitics.html"))
                            .MapValue(a => a.Name).ToElements.ByQuery(".div-antiparasitics").ToContent
                            .MapValue(a => a.DateApplied).ToElements.ByQuery(".span-antiparasitics-date").ToContent
                    .EndCollection
            .EndCollection;

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
