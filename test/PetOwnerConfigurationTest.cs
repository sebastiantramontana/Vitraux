using Vitraux.Helpers;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.TableRows;
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
        var nameValue = TestHelper.CreateValueModel((PetOwner a) => a.Name,
        [
            TestHelper.CreateTargetElement(new ElementIdSelector("petowner-name"), TestHelper.CreateContentElementPlace())
        ]);

        var templateForValue2 = TestHelper.CreateElementTemplateSelectorToId("petowner-address-template", "petowner-parent", ".petowner-address-target");

        var addressValue = TestHelper.CreateValueModel((PetOwner a) => a.Address,
        [
            TestHelper.CreateTargetElement(templateForValue2, TestHelper.CreateContentElementPlace()),
            TestHelper.CreateTargetElement(new ElementQuerySelector(".petwoner-address > div"), TestHelper.CreateAttributeElementPlace("data-petowner-address"))
        ]);

        var petsCollection = TestHelper.CreateCollectionTableModel(
            (PetOwner a) => a.pets,
            new ElementIdSelector("pets-table"),
            new TemplateRowSelector("pet-row-template"),
            [
                TestHelper.CreateValueModel((Pet m) => m.Name,
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".cell-pet-name"),TestHelper.CreateContentElementPlace()),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".anchor-cell-pet-name"),TestHelper.CreateAttributeElementPlace("href")),
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".another-anchor-cell-pet-name"),TestHelper.CreateAttributeElementPlace("href")),
                ]),
                TestHelper.CreateValueModel((Pet m) => ToDataUri(m.Photo),
                [
                    TestHelper.CreateTargetElement(new ElementQuerySelector(".pet-photo"), TestHelper.CreateAttributeElementPlace("src")),
                ])
            ],
            [
                TestHelper.CreateCollectionTableModel(
                (Pet m) => m.Vaccines,
                new ElementIdSelector("inner-table-vaccines"),
                new FetchRowSelector(new Uri("http://mysite.com/htmlparts/row-vaccines.html")),
                [
                    TestHelper.CreateValueModel((Vaccine v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".div-vaccine"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Vaccine v) => v.DateApplied,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".span-vaccine-date"),TestHelper.CreateContentElementPlace())
                    ])
                ],
                []),
                TestHelper.CreateCollectionTableModel(
                (Pet m) => m.Antiparasitics,
                new ElementQuerySelector("inner-table-antiparasitics"),
                new FetchRowSelector(new Uri("http://mysite.com/htmlparts/row-antiparasitics.html")),
                [
                    TestHelper.CreateValueModel((Antiparasitic v) => v.Name,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".div-antiparasitics"),TestHelper.CreateContentElementPlace())
                    ]),
                    TestHelper.CreateValueModel((Antiparasitic v) => v.DateApplied,
                    [
                        TestHelper.CreateTargetElement(new ElementQuerySelector(".span-antiparasitics-date"),TestHelper.CreateContentElementPlace())
                    ])
                ],
                [])
            ]);

        var sut = new PetOwnerConfiguration(dataUriConverter);

        var data = sut.ConfigureMapping(new ModelMapperRoot<PetOwner>());

        TestHelper.AssertValueModel(data.Values.ElementAt(0), nameValue, false);
        TestHelper.AssertValueModel(data.Values.ElementAt(1), addressValue, false);
        TestHelper.AssertCollectionTableModel(data.CollectionTables.ElementAt(0), petsCollection);
    }

    private string ToDataUri(byte[] data) => dataUriConverter.ToDataUri(MimeImage.Png, data);
}
