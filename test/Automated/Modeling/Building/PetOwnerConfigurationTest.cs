using Moq;
using Vitraux.Helpers;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Collections;
using Vitraux.Modeling.Data.Selectors.Values;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;
using Vitraux.Modeling.Data.Values;
using Vitraux.Test.Example;
using Vitraux.Test.Utils;

namespace Vitraux.Test.Modeling.Building;

public class PetOwnerConfigurationTest
{
    private readonly DataUriConverter dataUriConverter = new();

    [Fact]
    public void TestMapping()
    {
        var expectedData = CreatePetOwnerData();

        PetOwnerConfiguration sut = new(dataUriConverter);

        var serviceProvider = ServiceProviderMock.MockForPetOwner();

        var actualData = sut.ConfigureMapping(new ModelMapper<PetOwner>(serviceProvider));

        var actualDataSerialized = VitrauxDataSerializer.Serialize(actualData, 0);
        var expectedDataSerialized = VitrauxDataSerializer.Serialize(expectedData, 0);

        Assert.Equal(expectedDataSerialized, actualDataSerialized);
    }

    private string ToDataUri(byte[] data)
         => dataUriConverter.ToDataUri(MimeImage.Png, data);

    private ModelMappingData CreatePetOwnerData()
    {
        var petOwnerData = new ModelMappingData();

        var nameValue = CreateNameValueData();
        var addressValue = CreateAddressValueData();
        var phoneNumberValue = CreatePhoneNumberValueData();
        var suscriptionValue = CreateSubscriptionValueData();
        var commentsValue = CreateCommentsValueData();
        var petsCollection = CreatePetsCollectionData();

        petOwnerData.AddValue(nameValue);
        petOwnerData.AddValue(addressValue);
        petOwnerData.AddValue(phoneNumberValue);
        petOwnerData.AddValue(suscriptionValue);
        petOwnerData.AddValue(commentsValue);
        petOwnerData.AddCollection(petsCollection);

        return petOwnerData;
    }

    private CollectionData CreatePetsCollectionData()
    {
        var petsCollection = new CollectionData((PetOwner p) => p.Pets);

        var petsTarget1 = new CustomJsCollectionTarget("pets.manage")
        {
            ModuleFrom = new Uri("./modules/pets.js", UriKind.Relative)
        };

        var petsTarget2 = new CollectionTableTarget(new ElementIdSelectorString("pets-table"))
        {
            InsertionSelector = new TemplateInsertionSelectorId("pet-row-template")
        };

        FillPetsTableData(petsTarget2.Data);

        petsCollection.AddTarget(petsTarget1);
        petsCollection.AddTarget(petsTarget2);
        return petsCollection;
    }

    private static ValueData CreateSubscriptionValueData()
    {
        var suscriptionValue = new ValueData((PetOwner p) => p.Subscription);
        var suscriptionTarget = new OwnMappingTarget(typeof(Subscription));

        suscriptionValue.AddTarget(suscriptionTarget);
        return suscriptionValue;
    }

    private static ValueData CreatePhoneNumberValueData()
    {
        var phoneNumberValue = new ValueData((PetOwner p) => p.PhoneNumber);
        var phoneUri = new Uri("./htmlpieces/phoneblock.html", UriKind.Relative);

        var targetPhone1 = new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".petowner-phonenumber"),
            Place = new AttributeElementPlace("data-phonenumber"),
            Insertion = new InsertElementUriSelectorUri(phoneUri)
            {
                TargetChildElementsSelector = new ElementQuerySelectorString(".petowner-phonenumber-target")
            }
        };

        var targetPhone2 = new ElementValueTarget
        {
            Selector = new ElementIdSelectorString("petowner-phonenumber-id"),
            Place = ContentElementPlace.Instance
        };

        phoneNumberValue.AddTarget(targetPhone1);
        phoneNumberValue.AddTarget(targetPhone2);
        return phoneNumberValue;
    }

    private static ValueData CreateCommentsValueData()
    {
        var commentsValue = new ValueData((PetOwner p) => p.HtmlComments);

        var targetComments1 = new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".comments"),
            Place = HtmlElementPlace.Instance
        };

        commentsValue.AddTarget(targetComments1);

        return commentsValue;
    }


    private static ValueData CreateAddressValueData()
    {
        var addressValue = new ValueData((PetOwner p) => p.Address);

        var targetAddress1 = new ElementValueTarget
        {
            Selector = new ElementIdSelectorString("petowner-address-parent"),
            Insertion = new InsertElementTemplateSelectorId("petowner-address-template")
            {
                TargetChildElementsSelector = new ElementQuerySelectorString(".petowner-address-target")
            },
            Place = ContentElementPlace.Instance
        };

        var targetAddress2 = new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".petwoner-address > div"),
            Place = new AttributeElementPlace("data-petowner-address")
        };

        addressValue.AddTarget(targetAddress1);
        addressValue.AddTarget(targetAddress2);
        return addressValue;
    }

    private static ValueData CreateNameValueData()
    {
        var nameValue = new ValueData((PetOwner p) => p.Name);
        var targetName1 = new ElementValueTarget
        {
            Selector = new ElementIdSelectorString("petowner-name"),
            Place = ContentElementPlace.Instance
        };

        var targetName2 = new CustomJsValueTarget("poNameFunction") { ModuleFrom = new Uri("./modules/po.js", UriKind.Relative) };

        nameValue.AddTarget(targetName1);
        nameValue.AddTarget(targetName2);
        return nameValue;
    }

    private void FillPetsTableData(ModelMappingData petsTableData)
    {
        var petNameValue = CreatePetNameValue();
        var petVaccinesCollection = CreatePetVaccinesCollection();
        var petPhotoValue = CreatePetPhoto();
        var antiparasiticsCollection = CreatePetAntiparasiticsCollection();

        petsTableData.AddValue(petNameValue);
        petsTableData.AddCollection(petVaccinesCollection);
        petsTableData.AddValue(petPhotoValue);
        petsTableData.AddCollection(antiparasiticsCollection);
    }

    private static ValueData CreatePetNameValue()
    {
        var petNameValue = new ValueData((Pet p) => p.Name);

        petNameValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".cell-pet-name"),
            Place = ContentElementPlace.Instance
        });

        petNameValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".anchor-cell-pet-name"),
            Place = new AttributeElementPlace("href")
        });

        petNameValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".another-anchor-cell-pet-name"),
            Place = new AttributeElementPlace("href")
        });

        return petNameValue;
    }

    private static CollectionData CreatePetVaccinesCollection()
    {
        var vaccinesCollection = new CollectionData((Pet pet) => pet.Vaccines);
        var vaccineTarget = new CollectionTableTarget(new ElementQuerySelectorString(".inner-table-vaccines"))
        {
            InsertionSelector = new UriInsertionSelectorUri(new Uri("./htmlpieces/row-vaccines.html", UriKind.Relative)),
            TBodyIndex = 1
        };

        vaccinesCollection.AddTarget(vaccineTarget);

        FillVaccinesTableData(vaccineTarget.Data);

        return vaccinesCollection;
    }

    private static void FillVaccinesTableData(ModelMappingData vaccinesTableData)
    {
        var vaccineNameValue = new ValueData((Vaccine v) => v.Name);

        vaccineNameValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".div-vaccine"),
            Place = ContentElementPlace.Instance
        });

        var vaccineDateAppliedValue = new ValueData((Vaccine v) => v.DateApplied);
        vaccineDateAppliedValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".span-vaccine-date"),
            Place = ContentElementPlace.Instance
        });

        var vaccinesIngredientsCollection = new CollectionData((Vaccine v) => v.Ingredients);
        var ingredientsTarget = new CollectionElementTarget(new ElementQuerySelectorString(".ingredients-list"))
        {
            InsertionSelector = new TemplateInsertionSelectorId("ingredients-template"),
        };

        vaccinesIngredientsCollection.AddTarget(ingredientsTarget);

        FillIngredientsData(ingredientsTarget.Data);

        vaccinesTableData.AddValue(vaccineNameValue);
        vaccinesTableData.AddValue(vaccineDateAppliedValue);
        vaccinesTableData.AddCollection(vaccinesIngredientsCollection);
    }

    private static void FillIngredientsData(ModelMappingData ingredientsData)
    {
        var ingredientValue = new ValueData((string i) => i);
        ingredientValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".ingredient-item"),
            Place = ContentElementPlace.Instance
        });

        ingredientsData.AddValue(ingredientValue);
    }

    private ValueData CreatePetPhoto()
    {
        var petPhotoValue = new ValueData((Pet p) => ToDataUri(p.Photo));

        petPhotoValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".pet-photo"),
            Place = new AttributeElementPlace("src")
        });

        return petPhotoValue;
    }

    private static CollectionData CreatePetAntiparasiticsCollection()
    {
        var antiparasiticsCollection = new CollectionData((Pet pet) => pet.Antiparasitics);

        var antiparasiticsCustomJsTarget = new CustomJsCollectionTarget("globalThis.manageAntiparasitics");
        var antiparasiticsCollectionElementTarget = new CollectionElementTarget(new ElementQuerySelectorString(".inner-nav-antiparasitics"))
        {
            InsertionSelector = new UriInsertionSelectorUri(new Uri("./htmlpieces/row-antiparasitics.html", UriKind.Relative)),
        };

        FillAntiparasiticsCollectionElementData(antiparasiticsCollectionElementTarget.Data);

        antiparasiticsCollection.AddTarget(antiparasiticsCustomJsTarget);
        antiparasiticsCollection.AddTarget(antiparasiticsCollectionElementTarget);

        return antiparasiticsCollection;
    }

    private static void FillAntiparasiticsCollectionElementData(ModelMappingData antiparasiticsTableData)
    {
        var antiparasiticNameValue = new ValueData((Antiparasitic a) => a.Name);
        antiparasiticNameValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".div-antiparasitics"),
            Place = ContentElementPlace.Instance
        });

        var antiparasiticDateValue = new ValueData((Antiparasitic a) => a.DateApplied);
        antiparasiticDateValue.AddTarget(new ElementValueTarget
        {
            Selector = new ElementQuerySelectorString(".span-antiparasitics-date"),
            Place = ContentElementPlace.Instance
        });

        antiparasiticsTableData.AddValue(antiparasiticNameValue);
        antiparasiticsTableData.AddValue(antiparasiticDateValue);
    }
}