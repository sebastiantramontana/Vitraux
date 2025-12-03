using Moq;
using Vitraux.Helpers;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;
using Vitraux.Modeling.Data;
using Vitraux.Modeling.Data.Actions;
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
    private static readonly DataUriConverter dataUriConverter = new();

    [Fact]
    public void TestMapping()
    {
        var expectedData = CreatePetOwnerData();

        PetOwnerConfiguration sut = new(dataUriConverter);

        var actionKeyGenerator = new Mock<IActionKeyGenerator>();

        _ = actionKeyGenerator
            .Setup(g => g.Generate())
            .Returns("SomeKey");

        var serviceProvider = MockServiceProvider(actionKeyGenerator.Object);

        var actualData = sut.ConfigureMapping(new ModelMapper<PetOwner>(serviceProvider, actionKeyGenerator.Object));

        var ignoreParentActionData = new IgnoredProperty(typeof(ActionData), nameof(ActionTarget.Parent));
        var actualDataSerialized = VitrauxDataSerializer.Serialize(actualData, 0, [ignoreParentActionData]);
        var expectedDataSerialized = VitrauxDataSerializer.Serialize(expectedData, 0, [ignoreParentActionData]);

        Assert.Equal(expectedDataSerialized, actualDataSerialized);
    }

    private static IServiceProvider MockServiceProvider(IActionKeyGenerator actionKeyGenerator)
    {
        var serviceProviderMock = ServiceProviderMock.MockForPetOwner(actionKeyGenerator);

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IPetOwnerActionParameterBinder1)))
            .Returns(new PetOwnerActionParameterBinder1());

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IPetOwnerActionParameterBinder2)))
            .Returns(new PetOwnerActionParameterBinder2());

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(PetOwnerActionParameterBinder3)))
            .Returns(new PetOwnerActionParameterBinder3());

        _ = serviceProviderMock
            .Setup(sp => sp.GetService(typeof(PetOwnerActionParameterBinder4)))
            .Returns(new PetOwnerActionParameterBinder4());

        return serviceProviderMock.Object;
    }

    private static string ToDataUri(byte[] data)
         => dataUriConverter.ToDataUri(MimeImage.Png, data);

    private static ModelMappingData CreatePetOwnerData()
    {
        var petOwnerData = new ModelMappingData();

        var nameValue = CreateNameValueData();
        var addressValue = CreateAddressValueData();
        var phoneNumberValue = CreatePhoneNumberValueData();
        var suscriptionValue = CreateSubscriptionValueData();
        var commentsValue = CreateCommentsValueData();
        var petsCollection = CreatePetsCollectionData();
        var action1 = CreateAction1();
        var action2 = CreateAction2();
        var action3 = CreateAction3();
        var action4 = CreateAction4();
        var action5 = CreateAction5();
        var action6 = CreateAction6();
        var action7 = CreateAction7();
        var action8 = CreateAction8();

        petOwnerData.AddValue(nameValue);
        petOwnerData.AddValue(addressValue);
        petOwnerData.AddValue(phoneNumberValue);
        petOwnerData.AddValue(suscriptionValue);
        petOwnerData.AddValue(commentsValue);
        petOwnerData.AddCollection(petsCollection);
        petOwnerData.AddAction(action1);
        petOwnerData.AddAction(action2);
        petOwnerData.AddAction(action3);
        petOwnerData.AddAction(action4);
        petOwnerData.AddAction(action5);
        petOwnerData.AddAction(action6);
        petOwnerData.AddAction(action7);
        petOwnerData.AddAction(action8);

        return petOwnerData;
    }

    private static ActionData CreateAction1()
    {
        var action = new ActionData((PetOwner po) => po.Method1(), false, "SomeKey");

        AddActionTarget(action, new ElementQuerySelectorString("el1"), ["event1"]);

        return action;
    }

    private static ActionData CreateAction2()
    {
        var action = new ActionData((PetOwner po) => po.Method1(), false, "SomeKey");

        AddActionTarget(action, new ElementQuerySelectorString("el2"), ["event2"]);
        AddActionTarget(action, new ElementQuerySelectorString("el3"), ["event3", "event4"]);

        return action;
    }

    private static ActionData CreateAction3()
    {
        var action = new ActionData((PetOwner po) => po.Method2(), true, "SomeKey");

        AddActionTarget(action, new ElementQuerySelectorString("el4"), ["event5"]);

        return action;
    }

    private static ActionData CreateAction4()
    {
        var action = new ActionData((PetOwner po) => po.Method2(), true, "SomeKey");

        AddActionTarget(action, new ElementQuerySelectorString("el5"), ["event6", "event7"]);
        AddActionTarget(action, new ElementIdSelectorString("el6"), ["event8", "event9"]);

        return action;
    }

    private static ActionData CreateAction5()
    {
        IPetOwnerActionParameterBinder1 binder = new PetOwnerActionParameterBinder1();
        var action = new ActionData(binder, false, "SomeKey") { PassInputValueParameter = true };

        AddActionTarget(action, new ElementIdSelectorString("el7"), ["event10"]);

        return action;
    }

    private static ActionData CreateAction6()
    {
        IPetOwnerActionParameterBinder2 binder = new PetOwnerActionParameterBinder2();
        var action = new ActionData(binder, true, "SomeKey") { PassInputValueParameter = true };

        AddActionTarget(action, new ElementIdSelectorString("el8"), ["event11"]);

        AddActionParameter(action, "p1", new ElementQuerySelectorString("el9"), ValuePropertyElementPlace.Instance);
        AddActionParameter(action, "p2", new ElementIdSelectorString("el10"), ContentElementPlace.Instance);

        return action;
    }

    private static ActionData CreateAction7()
    {
        var binder = new PetOwnerActionParameterBinder3();
        var action = new ActionData(binder, true, "SomeKey") { PassInputValueParameter = true };

        AddActionTarget(action, new ElementIdSelectorString("el11"), ["event12"]);
        AddActionTarget(action, new ElementQuerySelectorString("el12"), ["event13", "event14"]);

        return action;
    }

    private static ActionData CreateAction8()
    {
        var binder = new PetOwnerActionParameterBinder4();
        var action = new ActionData(binder, false, "SomeKey") { PassInputValueParameter = true };

        AddActionTarget(action, new ElementIdSelectorString("el13"), ["event15"]);
        AddActionTarget(action, new ElementQuerySelectorString("el14"), ["event16", "event17"]);

        AddActionParameter(action, "p3", new ElementIdSelectorString("el15"), ValuePropertyElementPlace.Instance);
        AddActionParameter(action, "p4", new ElementQuerySelectorString("el16"), new AttributeElementPlace("att1"));

        return action;
    }

    private static void AddActionParameter(ActionData action, string name, ElementSelectorBase selector, ElementPlace place)
        => action.AddParameter(new ActionParameter(name)
        {
            Selector = selector,
            ElementPlace = place
        });

    private static void AddActionTarget(ActionData action, ElementSelectorBase selector, string[] events)
        => action.AddTarget(new ActionTarget(action)
        {
            Selector = selector,
            Events = events
        });

    private static CollectionData CreatePetsCollectionData()
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

    private static void FillPetsTableData(ModelMappingData petsTableData)
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

    private static ValueData CreatePetPhoto()
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