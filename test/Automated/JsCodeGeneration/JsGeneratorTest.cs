using Vitraux.Execution.Serialization;
using Vitraux.Execution.Tracking;
using Vitraux.Execution.ViewModelNames;
using Vitraux.Helpers;
using Vitraux.Test.Example;
using Vitraux.Test.Utils;

namespace Vitraux.Test.JsCodeGeneration;

public class JsGeneratorTest
{
    const string expectedInitializationJsForAtStart = """
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'e0');
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-address-parent', 'e1');
                                                globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'f2');
                                                globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petwoner-address > div', 'e3');
                                                globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petowner-phonenumber', 'e4');
                                                await globalThis.vitraux.storedElements.getFetchedElement('./htmlpieces/phoneblock.html', 'f5');
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', 'e6');
                                                globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.comments', 'e7');
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'e8');
                                                globalThis.vitraux.storedElements.getStoredTemplate('pet-row-template', 'c9');
                                                return Promise.resolve();
                                                """;

    const string expectedInitializationJsForOnDemand = """
                                                return Promise.resolve();
                                                """;

    const string expectedInitializationJsForAlways = """
                                                return Promise.resolve();
                                                """;

    const string expectedQueryElementsJsAtStart = """
                                        const e0 = globalThis.vitraux.storedElements.elements.e0;
                                        const e1 = globalThis.vitraux.storedElements.elements.e1;
                                        const f2 = globalThis.vitraux.storedElements.elements.f2;
                                        const e3 = globalThis.vitraux.storedElements.elements.e3;
                                        const e4 = globalThis.vitraux.storedElements.elements.e4;
                                        const f5 = globalThis.vitraux.storedElements.elements.f5;
                                        const e6 = globalThis.vitraux.storedElements.elements.e6;
                                        const e7 = globalThis.vitraux.storedElements.elements.e7;
                                        const e8 = globalThis.vitraux.storedElements.elements.e8;
                                        const c9 = globalThis.vitraux.storedElements.elements.c9;
                                        """;

    const string expectedQueryElementsJsOnDemand = """
                                        const e0 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'e0');
                                        const e1 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-address-parent', 'e1');
                                        const f2 = globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'f2');
                                        const e3 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petwoner-address > div', 'e3');
                                        const e4 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petowner-phonenumber', 'e4');
                                        const f5 = await globalThis.vitraux.storedElements.getFetchedElement('./htmlpieces/phoneblock.html', 'f5');
                                        const e6 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', 'e6');
                                        const e7 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.comments', 'e7');
                                        const e8 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'e8');
                                        const c9 = globalThis.vitraux.storedElements.getStoredTemplate('pet-row-template', 'c9');
                                        """;

    const string expectedQueryElementsJsAlways = """
                                      const e0 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-name');
                                      const e1 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-address-parent');
                                      const f2 = globalThis.vitraux.storedElements.getTemplate('petowner-address-template');
                                      const e3 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petwoner-address > div');
                                      const e4 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petowner-phonenumber');
                                      const f5 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/phoneblock.html');
                                      const e6 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-phonenumber-id');
                                      const e7 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.comments');
                                      const e8 = globalThis.vitraux.storedElements.getElementByIdAsArray('pets-table');
                                      const c9 = globalThis.vitraux.storedElements.getTemplate('pet-row-template');
                                      """;

    const string expectedCommonUpdateViewJs = """
                                        if(globalThis.vitraux.updating.utils.isValueValid(vm.v0)) {
                                            globalThis.vitraux.updating.dom.setElementsContent(e0, vm.v0);
                                            const m0 = await import('./modules/po.js');
                                            await m0.poNameFunction(vm.v0);
                                        }

                                        if(globalThis.vitraux.updating.utils.isValueValid(vm.v1)) {
                                            globalThis.vitraux.updating.dom.updateValueByInsertingElements(
                                                f2,
                                                e1,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-address-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.dom.setElementsContent(targetChildElements, vm.v1));
                                            globalThis.vitraux.updating.dom.setElementsAttribute(e3, 'data-petowner-address', vm.v1);
                                        }

                                        if(globalThis.vitraux.updating.utils.isValueValid(vm.v2)) {
                                            globalThis.vitraux.updating.dom.updateValueByInsertingElements(
                                                f5,
                                                e4,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-phonenumber-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.dom.setElementsAttribute(targetChildElements, 'data-phonenumber', vm.v2));
                                            globalThis.vitraux.updating.dom.setElementsContent(e6, vm.v2);
                                        }

                                        if(globalThis.vitraux.updating.utils.isValueValid(vm.v3)) {
                                            await globalThis.vitraux.updating.vmFunctions.executeUpdateViewFunction('Vitraux-Test-Example-Subscription', vm.v3);
                                        }

                                        if(globalThis.vitraux.updating.utils.isValueValid(vm.v4)) {
                                            globalThis.vitraux.updating.dom.setElementsHtml(e7, vm.v4);
                                        }

                                        if(globalThis.vitraux.updating.utils.isValueValid(vm.c0)) {
                                            const uc0 = async (p, item) =>
                                            {
                                                const n0_c0_e10 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.cell-pet-name');
                                                const n0_c0_e11 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.anchor-cell-pet-name');
                                                const n0_c0_e12 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.another-anchor-cell-pet-name');
                                                const n0_c0_e13 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.pet-photo');
                                                const n0_c0_e14 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.inner-table-vaccines');
                                                const n0_c0_c15 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/row-vaccines.html');
                                                const n0_c0_e16 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.inner-nav-antiparasitics');
                                                const n0_c0_c17 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/row-antiparasitics.html');

                                                if(globalThis.vitraux.updating.utils.isValueValid(item.v0)) {
                                                    globalThis.vitraux.updating.dom.setElementsContent(n0_c0_e10, item.v0);
                                                    globalThis.vitraux.updating.dom.setElementsAttribute(n0_c0_e11, 'href', item.v0);
                                                    globalThis.vitraux.updating.dom.setElementsAttribute(n0_c0_e12, 'href', item.v0);
                                                }

                                                if(globalThis.vitraux.updating.utils.isValueValid(item.v1)) {
                                                    globalThis.vitraux.updating.dom.setElementsAttribute(n0_c0_e13, 'src', item.v1);
                                                }

                                                if(globalThis.vitraux.updating.utils.isValueValid(item.c0)) {
                                                    const uc1 = async (p, item) =>
                                                    {
                                                        const n1_c0_e18 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.div-vaccine');
                                                        const n1_c0_e19 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.span-vaccine-date');
                                                        const n1_c0_e20 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.ingredients-list');
                                                        const n1_c0_c21 = globalThis.vitraux.storedElements.getTemplate('ingredients-template');

                                                        if(globalThis.vitraux.updating.utils.isValueValid(item.v0)) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(n1_c0_e18, item.v0);
                                                        }

                                                        if(globalThis.vitraux.updating.utils.isValueValid(item.v1)) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(n1_c0_e19, item.v1);
                                                        }

                                                        if(globalThis.vitraux.updating.utils.isValueValid(item.c0)) {
                                                            const uc2 = async (p, item) =>
                                                            {
                                                                const n2_c0_e22 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.ingredient-item');

                                                                if(globalThis.vitraux.updating.utils.isValueValid(item.v0)) {
                                                                    globalThis.vitraux.updating.dom.setElementsContent(n2_c0_e22, item.v0);
                                                                }

                                                                return Promise.resolve();
                                                            }

                                                            await globalThis.vitraux.updating.dom.updateCollectionByPopulatingElements(n1_c0_e20, n1_c0_c21, uc2, item.c0);
                                                        }

                                                        return Promise.resolve();
                                                    }

                                                    await globalThis.vitraux.updating.dom.updateTable(n0_c0_e14, 1, n0_c0_c15, uc1, item.c0);
                                                }

                                                if(globalThis.vitraux.updating.utils.isValueValid(item.c1)) {
                                                    const uc3 = async (p, item) =>
                                                    {
                                                        const n1_c1_e23 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.div-antiparasitics');
                                                        const n1_c1_e24 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.span-antiparasitics-date');

                                                        if(globalThis.vitraux.updating.utils.isValueValid(item.v0)) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(n1_c1_e23, item.v0);
                                                        }

                                                        if(globalThis.vitraux.updating.utils.isValueValid(item.v1)) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(n1_c1_e24, item.v1);
                                                        }

                                                        return Promise.resolve();
                                                    }

                                                    await globalThis.vitraux.updating.dom.updateCollectionByPopulatingElements(n0_c0_e16, n0_c0_c17, uc3, item.c1);

                                                    await globalThis.manageAntiparasitics(item.c1);
                                                }

                                                return Promise.resolve();
                                            }

                                            await globalThis.vitraux.updating.dom.updateTable(e8, 0, c9, uc0, vm.c0);

                                            const m1 = await import('./modules/pets.js');
                                            await m1.pets.manage(vm.c0);
                                        }

                                        return Promise.resolve();
                                        """;

    [Theory]
    [InlineData(QueryElementStrategy.OnlyOnceAtStart, expectedQueryElementsJsAtStart, expectedInitializationJsForAtStart)]
    [InlineData(QueryElementStrategy.OnlyOnceOnDemand, expectedQueryElementsJsOnDemand, expectedInitializationJsForOnDemand)]
    [InlineData(QueryElementStrategy.Always, expectedQueryElementsJsAlways, expectedInitializationJsForAlways)]
    public async Task GenerateCodeTest(QueryElementStrategy queryElementStrategy, string expectedQueryElementsJs, string expectedInitializationJs)
    {
        var sut = RootJsGeneratorFactory.Create();
        var petownerConfig = new PetOwnerConfiguration(new DataUriConverter());
        var serviceProvider = ServiceProviderMock.MockForPetOwner();

        var modelMapper = new ModelMapper<PetOwner>(serviceProvider);
        var data = petownerConfig.ConfigureMapping(modelMapper);
        var fullObjNames = RootJsGeneratorFactory.JsFullObjectNamesGenerator.Generate(data);

        var generatedJsCode = sut.GenerateJs(fullObjNames, queryElementStrategy);
        var expectedUpdateViewJs = expectedQueryElementsJs + Environment.NewLine + Environment.NewLine + expectedCommonUpdateViewJs;

        Assert.Equal(expectedUpdateViewJs, generatedJsCode.UpdateViewJs);
        Assert.Equal(expectedInitializationJs, generatedJsCode.InitializeViewJs);

        var serializationDataMapper = new ViewModelJsNamesMapper();
        var serializablePropertyValueExtractor = new SerializablePropertyValueExtractor();
        var jsonSerializer = new ViewModelJsonSerializer(RootJsGeneratorFactory.NotImplementedCaseGuard);

        var viewModelJsNames = serializationDataMapper.MapFromFull(fullObjNames);
        var vmJsNamesCache = new ViewModelJsNamesCacheGeneric<PetOwner>(serviceProvider);

        var tracker = new ViewModelNoChangesTracker<PetOwner>(serializablePropertyValueExtractor, vmJsNamesCache);
        var allData = tracker.Track(PetOwnerExample, viewModelJsNames);

        var serializedPetOwnerExampleJson = await jsonSerializer.Serialize(allData);

        Assert.Equal(ExpectedPetOwnerExampleJson, serializedPetOwnerExampleJson);
    }

    private static PetOwner PetOwnerExample { get; } =
        new PetOwner(
            "Juan",
            "123 Main St",
            "555-1234",
            "<h2>Some comments</h2>",
            new Subscription(SubscriptionFrequency.Semiannual, 123.456, true, true),
            [
                new Pet(
                    "Fido",
                    [1, 2, 3],
                    [
                        new Vaccine("Rabies", new DateTime(2022,6,8), ["Ingredient1", "Ingredient2"]),
                        new Vaccine("Distemper", new DateTime(2022,7,9), ["Ingredient3"])
                    ],
                    [
                        new Antiparasitic("Flea Treatment", new DateTime(2023,10,15)),
                        new Antiparasitic("Tick Treatment", new DateTime(2023,11,16))
                    ]),
                new Pet(
                    "Toulose",
                    [4, 5, 6],
                    [
                        new Vaccine("Feline Leukemia", new DateTime(2024,4,1), ["Ingredient4"])
                    ],
                    [
                        new Antiparasitic("Worm Treatment", new DateTime(2025,9,22))
                    ])
            ]);

    private const string ExpectedPetOwnerExampleJson =
        """
        {"v0":"Juan","v1":"123 Main St","v2":"555-1234","v3":{"v0":"Semiannual","v1":123.456,"v2":true,"v3":true},"v4":"\u003Ch2\u003ESome comments\u003C/h2\u003E","c0":[{"v0":"Fido","v1":"data:image/png;base64,AQID","c0":[{"v0":"Rabies","v1":"2022-06-08T00:00:00","c0":[{"v0":"Ingredient1"},{"v0":"Ingredient2"}]},{"v0":"Distemper","v1":"2022-07-09T00:00:00","c0":[{"v0":"Ingredient3"}]}],"c1":[{"v0":"Flea Treatment","v1":"2023-10-15T00:00:00"},{"v0":"Tick Treatment","v1":"2023-11-16T00:00:00"}]},{"v0":"Toulose","v1":"data:image/png;base64,BAUG","c0":[{"v0":"Feline Leukemia","v1":"2024-04-01T00:00:00","c0":[{"v0":"Ingredient4"}]}],"c1":[{"v0":"Worm Treatment","v1":"2025-09-22T00:00:00"}]}]}
        """;
}
