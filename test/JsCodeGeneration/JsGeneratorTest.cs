using Microsoft.VisualStudio.TestPlatform.Utilities;
using Swan;
using System.Buffers;
using System.Collections;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Test.Example;

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
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'e7');
                                                globalThis.vitraux.storedElements.getStoredTemplate('pet-row-template', 'c8');
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
                                        const c8 = globalThis.vitraux.storedElements.elements.c8;
                                        """;

    const string expectedQueryElementsJsOnDemand = """
                                        const e0 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'e0');
                                        const e1 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-address-parent', 'e1');
                                        const f2 = globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'f2');
                                        const e3 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petwoner-address > div', 'e3');
                                        const e4 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petowner-phonenumber', 'e4');
                                        const f5 = await globalThis.vitraux.storedElements.getFetchedElement('./htmlpieces/phoneblock.html', 'f5');
                                        const e6 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', 'e6');
                                        const e7 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'e7');
                                        const c8 = globalThis.vitraux.storedElements.getStoredTemplate('pet-row-template', 'c8');
                                        """;

    const string expectedQueryElementsJsAlways = """
                                      const e0 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-name');
                                      const e1 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-address-parent');
                                      const f2 = globalThis.vitraux.storedElements.getTemplate('petowner-address-template');
                                      const e3 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petwoner-address > div');
                                      const e4 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petowner-phonenumber');
                                      const f5 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/phoneblock.html');
                                      const e6 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-phonenumber-id');
                                      const e7 = globalThis.vitraux.storedElements.getElementByIdAsArray('pets-table');
                                      const c8 = globalThis.vitraux.storedElements.getTemplate('pet-row-template');
                                      """;

    const string expectedCommonUpdateViewJs = """
                                        if(vm.v0) {
                                            globalThis.vitraux.updating.dom.setElementsContent(e0, vm.v0);
                                            /*poNameFunction(vm.v0);*/
                                        }

                                        if(vm.v1) {
                                            globalThis.vitraux.updating.dom.updateValueByInsertingElements(
                                                f2,
                                                e1,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-address-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.dom.setElementsContent(targetChildElements, vm.v1));
                                            globalThis.vitraux.updating.dom.setElementsAttribute(e3, 'data-petowner-address', vm.v1);
                                        }

                                        if(vm.v2) {
                                            globalThis.vitraux.updating.dom.updateValueByInsertingElements(
                                                f5,
                                                e4,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-phonenumber-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.dom.setElementsAttribute(targetChildElements, 'data-phonenumber', vm.v2));
                                            globalThis.vitraux.updating.dom.setElementsContent(e6, vm.v2);
                                        }

                                        if(vm.v3) {

                                        }

                                        if(vm.c0) {
                                            const uc0 = async (p, item) =>
                                            {
                                                const vm_c0_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.cell-pet-name');
                                                const vm_c0_e1 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.anchor-cell-pet-name');
                                                const vm_c0_e2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.another-anchor-cell-pet-name');
                                                const vm_c0_e3 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.pet-photo');
                                                const vm_c0_e4 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.inner-table-vaccines');
                                                const vm_c0_c5 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/row-vaccines.html');
                                                const vm_c0_e6 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.inner-nav-antiparasitics');
                                                const vm_c0_c7 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/row-antiparasitics.html');

                                                if(item.v0) {
                                                    globalThis.vitraux.updating.dom.setElementsContent(vm_c0_e0, item.v0);
                                                    globalThis.vitraux.updating.dom.setElementsAttribute(vm_c0_e1, 'href', item.v0);
                                                    globalThis.vitraux.updating.dom.setElementsAttribute(vm_c0_e2, 'href', item.v0);
                                                }

                                                if(item.v1) {
                                                    globalThis.vitraux.updating.dom.setElementsAttribute(vm_c0_e3, 'src', item.v1);
                                                }

                                                if(item.c0) {
                                                    const uc1 = async (p, item) =>
                                                    {
                                                        const item_c0_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.div-vaccine');
                                                        const item_c0_e1 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.span-vaccine-date');
                                                        const item_c0_e2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.ingredients-list');
                                                        const item_c0_c3 = globalThis.vitraux.storedElements.getTemplate('ingredients-template');

                                                        if(item.v0) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(item_c0_e0, item.v0);
                                                        }

                                                        if(item.v1) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(item_c0_e1, item.v1);
                                                        }

                                                        if(item.c0) {
                                                            const uc2 = async (p, item) =>
                                                            {
                                                                const item_c0_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.ingredient-item');

                                                                if(item.v0) {
                                                                    globalThis.vitraux.updating.dom.setElementsContent(item_c0_e0, item.v0);
                                                                }

                                                                return Promise.resolve();
                                                            }

                                                            await globalThis.vitraux.updating.dom.updateCollectionByPopulatingElements(item_c0_e2, item_c0_c3, uc2, item.c0);
                                                        }

                                                        return Promise.resolve();
                                                    }

                                                    await globalThis.vitraux.updating.dom.updateTable(vm_c0_e4, vm_c0_c5, uc1, item.c0);
                                                }

                                                if(item.c1) {
                                                    const uc3 = async (p, item) =>
                                                    {
                                                        const item_c1_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.div-antiparasitics');
                                                        const item_c1_e1 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.span-antiparasitics-date');

                                                        if(item.v0) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(item_c1_e0, item.v0);
                                                        }

                                                        if(item.v1) {
                                                            globalThis.vitraux.updating.dom.setElementsContent(item_c1_e1, item.v1);
                                                        }

                                                        return Promise.resolve();
                                                    }

                                                    await globalThis.vitraux.updating.dom.updateCollectionByPopulatingElements(vm_c0_e6, vm_c0_c7, uc3, item.c1);
                                                }

                                                return Promise.resolve();
                                            }

                                            await globalThis.vitraux.updating.dom.updateTable(e7, c8, uc0, vm.c0);
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

        var modelMapper = new ModelMapper<PetOwner>();
        var data = petownerConfig.ConfigureMapping(modelMapper);

        var generatedJsCode = sut.GenerateJs(data, queryElementStrategy);
        var expectedUpdateViewJs = expectedQueryElementsJs + Environment.NewLine + Environment.NewLine + expectedCommonUpdateViewJs;

        Assert.Equal(expectedUpdateViewJs, generatedJsCode.UpdateViewInfo.JsCode);
        Assert.Equal(expectedInitializationJs, generatedJsCode.InitializeViewJs);

        var serializedPetOwnerExampleJson = await SerializeViewModelToJson(generatedJsCode.UpdateViewInfo.ViewModelSerializationData, PetOwnerExample);

        Assert.Equal(ExpectedPetOwnerExampleJson, serializedPetOwnerExampleJson);
    }

    private static PetOwner PetOwnerExample { get; } =
        new PetOwner(
            "Juan",
            "123 Main St",
            "555-1234",
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
        {"v0":"Juan","v1":"123 Main St","v2":"555-1234","v3":{},"c0":[{"v0":"Fido","v1":"data:image/png;base64,AQID","c0":[{"v0":"Rabies","v1":"8/6/2022 00:00:00","c0":[{"v0":"Ingredient1"},{"v0":"Ingredient2"}]},{"v0":"Distemper","v1":"9/7/2022 00:00:00","c0":[{"v0":"Ingredient3"}]}],"c1":[{"v0":"Flea Treatment","v1":"15/10/2023 00:00:00"},{"v0":"Tick Treatment","v1":"16/11/2023 00:00:00"}]},{"v0":"Toulose","v1":"data:image/png;base64,BAUG","c0":[{"v0":"Feline Leukemia","v1":"1/4/2024 00:00:00","c0":[{"v0":"Ingredient4"}]}],"c1":[{"v0":"Worm Treatment","v1":"22/9/2025 00:00:00"}]}]}
        """;

    private static async Task<string> SerializeViewModelToJson(ViewModelSerializationData viewModelSerializationData, object obj)
    {
        var buffer = new ArrayBufferWriter<byte>();
        await using var writer = new Utf8JsonWriter(buffer, new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.Default,
            Indented = false,
            IndentCharacter = ' ',
            IndentSize = 4,
            SkipValidation = true,
            NewLine = Environment.NewLine,
            MaxDepth = 0
        });

        SerializeObjectToJson(viewModelSerializationData, obj, writer);

        await writer.FlushAsync();

        return Encoding.UTF8.GetString(buffer.WrittenSpan);
    }

    private static void SerializeObjectToJson(ViewModelSerializationData viewModelSerializationData, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WriteStartObject();
        SerializePropertiesToJson(viewModelSerializationData, obj, utf8JsonWriter);
        utf8JsonWriter.WriteEndObject();
    }

    private static void SerializePropertiesToJson(ViewModelSerializationData viewModelSerializationData, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        SerializeValuesToJson(viewModelSerializationData.ValueProperties, obj, utf8JsonWriter);
        SerializeCollectionsToJson(viewModelSerializationData.CollectionProperties, obj, utf8JsonWriter);
    }

    private static void SerializeValuesToJson(IEnumerable<ValueViewModelSerializationData> values, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var value in values)
            SerializeValueToJson(value, obj, utf8JsonWriter);
    }

    private static void SerializeValueToJson(ValueViewModelSerializationData value, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        var retValue = value.ValuePropertyValueDelegate.DynamicInvoke(obj);
        SerializeObjectValueToJson(value.ValuePropertyName, retValue, utf8JsonWriter);
    }

    private static void SerializeObjectValueToJson(string propertyName, object? value, Utf8JsonWriter utf8JsonWriter)
    {
        if (value is null)
        {
            utf8JsonWriter.WriteNull(propertyName);
        }
        else
        {
            if (IsSimpleType(value.GetType()))
            {
                utf8JsonWriter.WriteString(propertyName, value.ToString());
            }
            else
            {
                //It will be handled when ToOwnMapping be implemented, in the meantime it returns an empty object here.
                utf8JsonWriter.WritePropertyName(propertyName);
                utf8JsonWriter.WriteStartObject();
                utf8JsonWriter.WriteEndObject();
            }
        }
    }

    private static bool IsSimpleType(Type type)
        => type.IsPrimitive ||
            type.IsEnum ||
            type == typeof(string) ||
            type == typeof(decimal) ||
            type == typeof(DateTime) ||
            type == typeof(DateTimeOffset) ||
            type == typeof(Guid) ||
            type == typeof(TimeSpan) ||
            type == typeof(DateOnly) ||
            type == typeof(TimeOnly) ||
            (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));


    private static void SerializeCollectionsToJson(IEnumerable<CollectionViewModelSerializationData> collections, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        foreach (var col in collections)
            SerializeCollectionToJson(col, obj, utf8JsonWriter);
    }

    private static void SerializeCollectionToJson(CollectionViewModelSerializationData collection, object obj, Utf8JsonWriter utf8JsonWriter)
    {
        utf8JsonWriter.WritePropertyName(collection.CollectionPropertyName);
        utf8JsonWriter.WriteStartArray();

        var collectionItems = GetTypedEnumerableObjectFromDelegateInvokation(collection.CollectionPropertyValueDelegate, obj);

        foreach (var child in collection.Children)
        {
            foreach (var item in collectionItems)
            {
                SerializeObjectToJson(child, item, utf8JsonWriter);
            }
        }

        utf8JsonWriter.WriteEndArray();
    }

    private static IEnumerable<object> GetTypedEnumerableObjectFromDelegateInvokation(Delegate del, object obj)
    {
        var enumerable = del.DynamicInvoke(obj) as IEnumerable;
        return enumerable?.Cast<object>() ?? [];
    }
}
