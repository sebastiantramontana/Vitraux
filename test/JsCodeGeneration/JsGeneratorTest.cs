using Moq;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Test.Example;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

namespace Vitraux.Test.JsCodeGeneration;

public class JsGeneratorTest
{
    const string expectedCodeAtStart = """
                                        const _element0 = globalThis.vitraux.storedElements.elements._element0;
                                        const _element1 = globalThis.vitraux.storedElements.elements._element1;
                                        const _from2 = globalThis.vitraux.storedElements.elements._from2;
                                        const _element3 = globalThis.vitraux.storedElements.elements._element3;
                                        const _element4 = globalThis.vitraux.storedElements.elements._element4;
                                        const _from5 = globalThis.vitraux.storedElements.elements._from5;
                                        const _element6 = globalThis.vitraux.storedElements.elements._element6;
                                        const _element7 = globalThis.vitraux.storedElements.elements._element7;
                                        const _coll8 = globalThis.vitraux.storedElements.elements._coll8;
                                        """;

    const string expectedCodeOnDemand = """
                                        const _element0 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', '_element0');
                                        const _element1 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-address-parent', '_element1');
                                        const _from2 = globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', '_from2');
                                        const _element3 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petwoner-address > div', '_element3');
                                        const _element4 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petowner-phonenumber', '_element4');
                                        const _from5 = await globalThis.vitraux.storedElements.getFetchedElement('./htmlpieces/phoneblock', '_from5');
                                        const _element6 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', '_element6');
                                        const _element7 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', '_element7');
                                        const _coll8 = globalThis.vitraux.storedElements.getStoredTemplate('pet-row-template', '_coll8');
                                        """;

    const string expectedCodeAlways = """
                                      const _element0 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-name');
                                      const _element1 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-address-parent');
                                      const _from2 = globalThis.vitraux.storedElements.getTemplate('petowner-address-template');
                                      const _element3 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petwoner-address > div');
                                      const _element4 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petowner-phonenumber');
                                      const _from5 = await globalThis.vitraux.storedElements.fetchElement('./htmlpieces/phoneblock');
                                      const _element6 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-phonenumber-id');
                                      const _element7 = globalThis.vitraux.storedElements.getElementByIdAsArray('pets-table');
                                      const _coll8 = globalThis.vitraux.storedElements.getTemplate('pet-row-template');
                                      """;

    const string expectedExecutedCodeForAtStart = """
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', '_element0');
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-address-parent', '_element1');
                                                globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', '_from2');
                                                globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petwoner-address > div', '_element3');
                                                globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, '.petowner-phonenumber', '_element4');
                                                await globalThis.vitraux.storedElements.getFetchedElement('./htmlpieces/phoneblock', '_from5');
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', '_element6');
                                                globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', '_element7');
                                                globalThis.vitraux.storedElements.getStoredTemplate('pet-row-template', '_coll8');
                                                """;

    const string expectedCodeForValues = """
                                        if(vm.value0) {
                                            globalThis.vitraux.updating.setElementsContent(_element0, vm.value0);
                                            poNameFunction(vm.value0);
                                        }

                                        if(vm.value1) {
                                            globalThis.vitraux.updating.UpdateValueByInsertingElements(
                                                _from2,
                                                _element1,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-address-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.setElementsContent(targetChildElements, vm.value1));
                                            globalThis.vitraux.updating.setElementsAttribute(_element3, 'data-petowner-address', vm.value1);
                                        }

                                        if(vm.value2) {
                                            globalThis.vitraux.updating.UpdateValueByInsertingElements(
                                                _from5,
                                                _element4,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-phonenumber-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.setElementsAttribute(targetChildElements, 'data-phonenumber', vm.value2));
                                            globalThis.vitraux.updating.setElementsContent(_element6, vm.value2);
                                        }

                                        if(vm.value3) {

                                        }
                                        """;

    [Theory]
    [InlineData(QueryElementStrategy.OnlyOnceAtStart, expectedCodeAtStart)]
    [InlineData(QueryElementStrategy.OnlyOnceOnDemand, expectedCodeOnDemand)]
    [InlineData(QueryElementStrategy.Always, expectedCodeAlways)]
    public void GenerateCodeTest(QueryElementStrategy queryElementStrategy, string expectedQueryElementsCode)
    {
        var executorMock = new Mock<IJsCodeExecutor>();

        var sut = CreateSut(executorMock.Object);
        var personaConfig = new PetOwnerConfiguration(new DataUriConverter());

        var modelMapper = new ModelMapper<PetOwner>();
        var data = personaConfig.ConfigureMapping(modelMapper);

        var actualCode = sut.GenerateJsCode(data, queryElementStrategy);
        var expectedCode = expectedQueryElementsCode + Environment.NewLine + Environment.NewLine + expectedCodeForValues;

        actualCode.Should().Be(expectedCode);

        if (queryElementStrategy == QueryElementStrategy.OnlyOnceAtStart)
            executorMock.Verify(e => e.Excute(expectedExecutedCodeForAtStart), Times.Once);
    }

    //[Fact]
    //public void SampleToTestGeneratedJsCode()
    //{
    //    //Arrange
    //    var html = """
    //        <!DOCTYPE html>

    //        <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
    //        <head>
    //            <meta charset="utf-8" />
    //            <title>Some Title</title>
    //        </head>
    //        <body>
    //        <div id="test_id">text to change</div>
    //        </body>
    //        </html>
    //        """;

    //    var options = new ChromeOptions();
    //    // Avoid opening a Chrome window browser: The test runs in memory
    //    options.AddArguments("--disable-crash-reporter", "--enable-gpu", "--force-headless-for-tests", "--headless");

    //    //Also: FireFoxDriver, EdgeDriver and SafariDriver
    //    IWebDriver driver = new ChromeDriver(options);

    //    //Load a page with the desired html
    //    driver.Navigate().GoToUrl("about:blank"); //First blank page...
    //    var js = (IJavaScriptExecutor)driver;
    //    js.ExecuteScript($"document.write(arguments[0]);", html); //then, write the html

    //    //Act
    //    js.ExecuteScript("document.getElementById('test_id').innerHTML = 'text changed'");

    //    //Assert
    //    var element = driver.FindElement(By.Id("test_id"));
    //    element.Text.Should().Be("text changed");
    //}

    private static RootJsGenerator CreateSut(IJsCodeExecutor jsCodeExecutor)
    {
        var getElementByIdAsArrayCall = new GetElementByIdAsArrayCall();
        var getElementsByQuerySelectorCall = new GetElementsByQuerySelectorCall();
        var getStoredTemplateCall = new GetStoredTemplateCall();
        var getFetchedElementCall = new GetFetchedElementCall();
        var notImplementedCaseGuard = new NotImplementedCaseGuard();

        var queryElementsGeneratorByStrategyContext = CreateQueryElementsJsCodeGeneratorByStrategyContext(jsCodeExecutor,
                                                                                                          getElementByIdAsArrayCall,
                                                                                                          getElementsByQuerySelectorCall,
                                                                                                          getStoredTemplateCall,
                                                                                                          getFetchedElementCall,
                                                                                                          notImplementedCaseGuard);
        var uniqueSelectorsFilter = new UniqueSelectorsFilter();
        var elementNamesGenerator = new JsObjectNamesGenerator(notImplementedCaseGuard);
        var valueNamesGenerator = new ValueNamesGenerator();
        var collectionNamesGenerator = new CollectionNamesGenerator();
        var codeFormatter = new CodeFormatter();
        var propertyCheckerJsCodeGeneration = new PropertyCheckerJsCodeGeneration(codeFormatter);

        var valueJsCodeGenerator = CreateValuesJsCodeGenerationBuilder(getElementsByQuerySelectorCall,
                                                                       propertyCheckerJsCodeGeneration,
                                                                       codeFormatter,
                                                                       notImplementedCaseGuard);
        //var collectionsJsCodeGenerationBuilder = CreateCollectionsJsCodeGenerationBuilder(propertyCheckerJsCodeGeneration, codeFormatter);
        var jsGenerator = new JsGenerator(uniqueSelectorsFilter, elementNamesGenerator, valueNamesGenerator, collectionNamesGenerator, valueJsCodeGenerator, /*collectionsJsCodeGenerationBuilder,*/ queryElementsGeneratorByStrategyContext);

        return new RootJsGenerator(jsGenerator);
    }

    private static QueryElementsJsCodeGeneratorByStrategyContext CreateQueryElementsJsCodeGeneratorByStrategyContext(
        IJsCodeExecutor jsCodeExecutor,
        IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
        IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
        IGetStoredTemplateCall getStoredTemplateCall,
        IGetFetchedElementCall getFetchedElementCall,
        INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var builder = new QueryElementsJsGenerator();
        var getStoredElementByIdAsArrayCall = new GetStoredElementByIdAsArrayCall();
        var getStoredElementsByQuerySelectorCall = new GetStoredElementsByQuerySelectorCall();

        var atStartGenerator = CreateAtStartGenerator(builder,
                                                      jsCodeExecutor,
                                                      getStoredElementByIdAsArrayCall,
                                                      getStoredElementsByQuerySelectorCall,
                                                      getStoredTemplateCall,
                                                      getFetchedElementCall,
                                                      notImplementedCaseGuard);

        var onDemandGenerator = CreateOnDemandGenerator(builder,
                                                        getStoredElementByIdAsArrayCall,
                                                        getStoredElementsByQuerySelectorCall,
                                                        getStoredTemplateCall,
                                                        getFetchedElementCall,
                                                        notImplementedCaseGuard);

        var onAlwaysGenerator = CreateAlwaysGenerator(builder,
                                                      getElementByIdAsArrayCall,
                                                      getElementsByQuerySelectorCall,
                                                      notImplementedCaseGuard);

        return new QueryElementsJsCodeGeneratorByStrategyContext(atStartGenerator, onDemandGenerator, onAlwaysGenerator);
    }

    //private static ICollectionsJsCodeGenerationBuilder CreateCollectionsJsCodeGenerationBuilder(IPropertyCheckerJsCodeGeneration propertyCheckerJsCodeGeneration, ICodeFormatter codeFormatter)
    //{
    //    var randomStringGenerator = new RandomStringGenerator();
    //    var updateCollectionFunctionCallbackJsCodeGenerator = new UpdateCollectionFunctionCallbackJsCodeGenerator(randomStringGenerator, codeFormatter);
    //    var updateCollectionByPopulatingElementsCall = new UpdateCollectionByPopulatingElementsCall();
    //    var updateTableCall = new UpdateTableCall();
    //    var updateCollectionJsCodeGenerator = new UpdateCollectionJsCodeGenerator(updateTableCall, updateCollectionByPopulatingElementsCall, updateCollectionFunctionCallbackJsCodeGenerator);

    //    return new CollectionsJsCodeGenerationBuilder(propertyCheckerJsCodeGeneration, updateCollectionJsCodeGenerator);
    //}

    private static IValuesJsCodeGenerationBuilder CreateValuesJsCodeGenerationBuilder(
        IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
        IPropertyCheckerJsCodeGeneration propertyCheckerJsCodeGeneration,
        ICodeFormatter codeFormatter,
        INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var setElementsAttributeCall = new SetElementsAttributeCall();
        var attributeCodeGenerator = new ElementPlaceAttributeJsGenerator(setElementsAttributeCall);

        var setElementsContentCall = new SetElementsContentCall();
        var contentCodeGenerator = new ElementPlaceContentJsGenerator(setElementsContentCall);

        var toChildQueryFunctionCall = new ToChildQueryFunctionCall(getElementsByQuerySelectorCall);
        var updateByPopulatingElementsCall = new UpdateValueByInsertingElementsCall(codeFormatter);
        var updateChildElementsFunctionCall = new UpdateChildElementsFunctionCall(setElementsAttributeCall, setElementsContentCall, notImplementedCaseGuard);

        var targetElementDirectUpdateValueJsCodeGenerator = new TargetElementsDirectUpdateValueJsGenerator(attributeCodeGenerator, contentCodeGenerator, notImplementedCaseGuard);
        var targetByPopulatingElementsUpdateValueJsCodeGenerator = new TargetElementsUpdateValueInsertJsGenerator(updateByPopulatingElementsCall, toChildQueryFunctionCall, updateChildElementsFunctionCall, notImplementedCaseGuard);
        var targetElementsValueJsCodeGenerationBuilder = new TargetElementsValueJsGenerator(targetElementDirectUpdateValueJsCodeGenerator, targetByPopulatingElementsUpdateValueJsCodeGenerator, notImplementedCaseGuard);

        return new ValuesJsCodeGenerationBuilder(propertyCheckerJsCodeGeneration, targetElementsValueJsCodeGenerationBuilder);
    }

    private static QueryElementsOnlyOnceAtStartJsGenerator CreateAtStartGenerator(
        IQueryElementsJsGenerator builder,
        IJsCodeExecutor jsCodeExecutor,
        IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
        IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
        IGetStoredTemplateCall getStoredTemplateCall,
        IGetFetchedElementCall getFetchedElementCall,
        INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var storageElementJsLineGeneratorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
        var storageElementJsLineGeneratorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
        var storageElementJsLineGeneratorByTemplate = new StorageElementJsLineGeneratorByTemplate(getStoredTemplateCall);
        var storageElementJsLineGeneratorByUri = new StorageElementJsLineGeneratorByUri(getFetchedElementCall);

        var storageElementJsLineGeneratorElementsById = new StorageElementJsLineGeneratorElementsById(storageElementJsLineGeneratorById, notImplementedCaseGuard);
        var storageElementJsLineGeneratorElementsByQuery = new StorageElementJsLineGeneratorElementsByQuery(storageElementJsLineGeneratorByQuerySelector, notImplementedCaseGuard);
        var storageElementJsLineGeneratorInsertElementsByTemplate = new StorageElementJsLineGeneratorInsertElementsByTemplate(storageElementJsLineGeneratorByTemplate, notImplementedCaseGuard);
        var storageElementJsLineGeneratorInsertElementsByUri = new StorageElementJsLineGeneratorInsertElementsByUri(storageElementJsLineGeneratorByUri, notImplementedCaseGuard);

        var storageElementValueLineGenerator = new StorageElementValueJsLineGenerator(storageElementJsLineGeneratorElementsById,
                                                                                      storageElementJsLineGeneratorElementsByQuery,
                                                                                      storageElementJsLineGeneratorInsertElementsByTemplate,
                                                                                      storageElementJsLineGeneratorInsertElementsByUri,
                                                                                      notImplementedCaseGuard);

        var jsLineGeneratorCollectionByTemplate = new StorageElementCollectionJsLineGeneratorByTemplate(storageElementJsLineGeneratorByTemplate, notImplementedCaseGuard);
        var jsLineGeneratorCollectionByUri = new StorageElementCollectionJsLineGeneratorByUri(storageElementJsLineGeneratorByUri, notImplementedCaseGuard);
        var storageElementCollectionLineGenerator = new StorageElementCollectionJsLineGenerator(jsLineGeneratorCollectionByTemplate, jsLineGeneratorCollectionByUri, notImplementedCaseGuard);

        var storageElementsGenerator = new StoreElementsJsCodeGenerator(storageElementValueLineGenerator, storageElementCollectionLineGenerator, notImplementedCaseGuard);
        var initializer = new QueryElementsOnlyOnceAtStartup(storageElementsGenerator, jsCodeExecutor);
        var atStartDeclaringGenerator = new QueryElementsDeclaringOnlyOnceAtStartJsGenerator();

        return new QueryElementsOnlyOnceAtStartJsGenerator(builder, atStartDeclaringGenerator, initializer);
    }

    private static QueryElementsOnlyOnceOnDemandJsCodeGenerator CreateOnDemandGenerator(
        IQueryElementsJsGenerator builder,
        IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
        IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
        IGetStoredTemplateCall getStoredTemplateCall,
        IGetFetchedElementCall getFetchedElementCall,
        INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var declaringOnlyOncenDemandByIdGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator(getStoredElementByIdAsArrayCall, notImplementedCaseGuard);
        var declaringOnlyOnceOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator(getStoredElementsByQuerySelectorCall, notImplementedCaseGuard);
        var declaringOnlyOnceOnDemandValueByTemplateGenerator = new QueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator(getStoredTemplateCall, notImplementedCaseGuard);
        var declaringOnlyOnceOnDemandValueByUriGenerator = new QueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator(getFetchedElementCall, notImplementedCaseGuard);
        var declaringOnlyOnceOnDemandCollectionByTemplateGenerator = new QueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator(getStoredTemplateCall, notImplementedCaseGuard);
        var declaringOnlyOnceOnDemandCollectionByUriGenerator = new QueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator(getFetchedElementCall, notImplementedCaseGuard);

        var onDemandGeneratorContext = new JsQueryElementsOnlyOnceOnDemandGeneratorContext(declaringOnlyOncenDemandByIdGenerator,
                                                                                           declaringOnlyOnceOnDemandByQuerySelectorGenerator,
                                                                                           declaringOnlyOnceOnDemandValueByTemplateGenerator,
                                                                                           declaringOnlyOnceOnDemandValueByUriGenerator,
                                                                                           declaringOnlyOnceOnDemandCollectionByTemplateGenerator,
                                                                                           declaringOnlyOnceOnDemandCollectionByUriGenerator,
                                                                                           notImplementedCaseGuard);

        var declaringOnlyOnceOnDemandGenerator = new QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(onDemandGeneratorContext);

        return new QueryElementsOnlyOnceOnDemandJsCodeGenerator(builder, declaringOnlyOnceOnDemandGenerator);
    }

    private static QueryElementsAlwaysJsCodeGenerator CreateAlwaysGenerator(
        IQueryElementsJsGenerator builder,
        IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
        IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
        INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var getTemplateCall = new GetTemplateCall();
        var fetchElementCall = new FetchElementCall();

        var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsGenerator(getElementByIdAsArrayCall, notImplementedCaseGuard);
        var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsGenerator(getElementsByQuerySelectorCall, notImplementedCaseGuard);
        var declaringAlwaysValueByTemplateGenerator = new QueryElementsDeclaringAlwaysValueByTemplateJsGenerator(getTemplateCall, notImplementedCaseGuard);
        var declaringAlwaysValueByUriGenerator = new QueryElementsDeclaringAlwaysValueByUriJsGenerator(fetchElementCall, notImplementedCaseGuard);
        var declaringAlwaysCollectionByTemplate = new QueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator(getTemplateCall, notImplementedCaseGuard);
        var declaringAlwaysCollectionByUri = new QueryElementsDeclaringAlwaysCollectionByUriJsGenerator(fetchElementCall, notImplementedCaseGuard);

        var alwaysGeneratorContext = new JsQueryElementsDeclaringAlwaysGeneratorContext(declaringAlwaysByIdGenerator,
                                                                                        declaringAlwaysByQuerySelectorGenerator,
                                                                                        declaringAlwaysValueByTemplateGenerator,
                                                                                        declaringAlwaysValueByUriGenerator,
                                                                                        declaringAlwaysCollectionByTemplate,
                                                                                        declaringAlwaysCollectionByUri,
                                                                                        notImplementedCaseGuard);

        var declaringAlwaysGenerator = new QueryElementsDeclaringAlwaysCodeGenerator(alwaysGeneratorContext);

        return new QueryElementsAlwaysJsCodeGenerator(declaringAlwaysGenerator, builder);
    }
}
