using Vitraux.Helpers;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Insertion;
using Vitraux.Modeling.Models;
using Vitraux.Test.Example;
using Vitraux.Test.Modeling;

namespace Vitraux.Test;

[TestFixture]
public class PetOwnerConfigurationTest
{
    private readonly DataUriConverter dataUriConverter = new();

    [Test]
    public void TestMapping()
    {
        ValueModel nameValue = TestHelper.CreateValueModel((PetOwner a) => a.Name,
        [
            TestHelper.CreateTargetElement(new ElementIdSelectorString("petowner-name"), TestHelper.CreateContentElementPlace())
        ]);

        Vitraux.Modeling.Building.Selectors.Elements.Populating.ElementTemplateSelectorString templateForValue2 = TestHelper.CreateElementTemplateSelectorToId("petowner-address-template", "petowner-parent", ".petowner-address-target");

        ValueModel addressValue = TestHelper.CreateValueModel((PetOwner a) => a.Address,
        [
            TestHelper.CreateTargetElement(templateForValue2, TestHelper.CreateContentElementPlace()),
            TestHelper.CreateTargetElement(new ElementQuerySelectorString(".petwoner-address > div"), TestHelper.CreateAttributeElementPlace("data-petowner-address"))
        ]);

        CollectionTableModel petsCollection = TestHelper.CreateCollectionTableModel(
            (PetOwner a) => a.pets,
            new ElementIdSelectorString("pets-table"),
            new TemplateInsertionSelectorString("pet-row-template"),
            [
                TestHelper.CreateValueModel((Pet m) => m.Name,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelectorString(".cell-pet-name"),TestHelper.CreateContentElementPlace()),
                    TestHelper.CreateTargetElement(new ElementQuerySelectorString(".anchor-cell-pet-name"),TestHelper.CreateAttributeElementPlace("href")),
                    TestHelper.CreateTargetElement(new ElementQuerySelectorString(".another-anchor-cell-pet-name"),TestHelper.CreateAttributeElementPlace("href")),
                ]),
                TestHelper.CreateValueModel((Pet m) => ToDataUri(m.Photo),
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelectorString(".pet-photo"), TestHelper.CreateAttributeElementPlace("src")),
                ])
            ],
            [
                TestHelper.CreateCollectionTableModel(
                (Pet m) => m.Vaccines,
                new ElementIdSelectorString("inner-table-vaccines"),
                new FetchInsertionSelectorUri(new Uri("http://mysite.com/htmlparts/row-vaccines.html")),
                [
                    TestHelper.CreateValueModel((Vaccine v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelectorString(".div-vaccine"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Vaccine v) => v.DateApplied,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelectorString(".span-vaccine-date"),TestHelper.CreateContentElementPlace())
                    ])
                ],
                []),
                TestHelper.CreateCollectionElementModel(
                (Pet m) => m.Antiparasitics,
                new ElementQuerySelectorString("inner-nav-antiparasitics"),
                new FetchInsertionSelectorUri(new Uri("http://mysite.com/htmlparts/row-antiparasitics.html")),
                [
                    TestHelper.CreateValueModel((Antiparasitic v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelectorString(".div-antiparasitics"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Antiparasitic v) => v.DateApplied,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelectorString(".span-antiparasitics-date"),TestHelper.CreateContentElementPlace())
                    ])
                ],
                [])
            ]);

        PetOwnerConfiguration sut = new(dataUriConverter);

        IModelMappingData data = sut.ConfigureMapping(new ModelMapperRoot<PetOwner>());

        TestHelper.AssertValueModel(data.Values.ElementAt(0), nameValue, false);
        TestHelper.AssertValueModel(data.Values.ElementAt(1), addressValue, false);
        TestHelper.AssertCollectionElementModel(data.CollectionElements.Cast<CollectionTableModel>().ElementAt(0), petsCollection);
    }

    private string ToDataUri(byte[] data)
    {
        return dataUriConverter.ToDataUri(MimeImage.Png, data);
    }
}
