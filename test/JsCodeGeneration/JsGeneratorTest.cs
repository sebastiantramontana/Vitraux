using Moq;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Test.Example;

namespace Vitraux.Test.JsCodeGeneration;

public class JsGeneratorTest
{
    const string expectedCodeAtStart = """
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

    const string expectedCodeOnDemand = """
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

    const string expectedCodeAlways = """
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

    const string expectedExecutedCodeForAtStart = """
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

    const string expectedCodeForUpdating = """
                                        if(vm.v0) {
                                            globalThis.vitraux.updating.setElementsContent(e0, vm.v0);
                                            /*poNameFunction(vm.v0);*/
                                        }

                                        if(vm.v1) {
                                            globalThis.vitraux.updating.UpdateValueByInsertingElements(
                                                f2,
                                                e1,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-address-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.setElementsContent(targetChildElements, vm.v1));
                                            globalThis.vitraux.updating.setElementsAttribute(e3, 'data-petowner-address', vm.v1);
                                        }

                                        if(vm.v2) {
                                            globalThis.vitraux.updating.UpdateValueByInsertingElements(
                                                f5,
                                                e4,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-phonenumber-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.setElementsAttribute(targetChildElements, 'data-phonenumber', vm.v2));
                                            globalThis.vitraux.updating.setElementsContent(e6, vm.v2);
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
                                                    globalThis.vitraux.updating.setElementsContent(vm_c0_e0, item.v0);
                                                    globalThis.vitraux.updating.setElementsAttribute(vm_c0_e1, 'href', item.v0);
                                                    globalThis.vitraux.updating.setElementsAttribute(vm_c0_e2, 'href', item.v0);
                                                }

                                                if(item.v1) {
                                                    globalThis.vitraux.updating.setElementsAttribute(vm_c0_e3, 'src', item.v1);
                                                }

                                                if(item.c0) {
                                                    const uc1 = async (p, item) =>
                                                    {
                                                        const item_c0_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.div-vaccine');
                                                        const item_c0_e1 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.span-vaccine-date');
                                                        const item_c0_e2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.ingredients-list');
                                                        const item_c0_c3 = globalThis.vitraux.storedElements.getTemplate('ingredients-template');

                                                        if(item.v0) {
                                                            globalThis.vitraux.updating.setElementsContent(item_c0_e0, item.v0);
                                                        }

                                                        if(item.v1) {
                                                            globalThis.vitraux.updating.setElementsContent(item_c0_e1, item.v1);
                                                        }

                                                        if(item.c0) {
                                                            const uc2 = async (p, item) =>
                                                            {
                                                                const item_c0_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.ingredient-item');

                                                                if(item.v0) {
                                                                    globalThis.vitraux.updating.setElementsContent(item_c0_e0, item.v0);
                                                                }

                                                                return Promise.resolve();
                                                            }

                                                            await globalThis.vitraux.updating.UpdateCollectionByPopulatingElements(item_c0_e2, item_c0_c3, uc2, item.c0);
                                                        }

                                                        return Promise.resolve();
                                                    }

                                                    await globalThis.vitraux.updating.UpdateTable(vm_c0_e4, vm_c0_c5, uc1, item.c0);
                                                }

                                                if(item.c1) {
                                                    const uc3 = async (p, item) =>
                                                    {
                                                        const item_c1_e0 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.div-antiparasitics');
                                                        const item_c1_e1 = globalThis.vitraux.storedElements.getElementsByQuerySelector(p, '.span-antiparasitics-date');

                                                        if(item.v0) {
                                                            globalThis.vitraux.updating.setElementsContent(item_c1_e0, item.v0);
                                                        }

                                                        if(item.v1) {
                                                            globalThis.vitraux.updating.setElementsContent(item_c1_e1, item.v1);
                                                        }

                                                        return Promise.resolve();
                                                    }

                                                    await globalThis.vitraux.updating.UpdateCollectionByPopulatingElements(vm_c0_e6, vm_c0_c7, uc3, item.c1);
                                                }

                                                return Promise.resolve();
                                            }

                                            await globalThis.vitraux.updating.UpdateTable(e7, c8, uc0, vm.c0);
                                        }

                                        return Promise.resolve();
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

        var actualCode = sut.GenerateJs(data, queryElementStrategy);
        var expectedCode = expectedQueryElementsCode + Environment.NewLine + Environment.NewLine + expectedCodeForUpdating;

        Assert.Equal(expectedCode, actualCode);

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

        var collectionsJsCodeGenerationBuilder = CreateCollectionsJsCodeGenerationBuilder(propertyCheckerJsCodeGeneration, codeFormatter);

        var promiseJsGenerator = new PromiseJsGenerator();

        var jsGenerator = new JsGenerator(uniqueSelectorsFilter, elementNamesGenerator, valueNamesGenerator, collectionNamesGenerator, valueJsCodeGenerator, collectionsJsCodeGenerationBuilder, queryElementsGeneratorByStrategyContext, promiseJsGenerator);

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

    private static ICollectionsJsGenerationBuilder CreateCollectionsJsCodeGenerationBuilder(IPropertyCheckerJsCodeGeneration propertyCheckerJsCodeGeneration, ICodeFormatter codeFormatter)
    {
        var randomStringGenerator = new CollectionUpdateFunctionNameGenerator();
        var updateCollectionFunctionCallbackJsCodeGenerator = new UpdateCollectionFunctionCallbackJsCodeGenerator(randomStringGenerator, codeFormatter);
        var updateCollectionByPopulatingElementsCall = new UpdateCollectionByPopulatingElementsCall();
        var updateTableCall = new UpdateTableCall();
        var updateCollectionJsCodeGenerator = new UpdateCollectionJsCodeGenerator(updateTableCall, updateCollectionByPopulatingElementsCall, updateCollectionFunctionCallbackJsCodeGenerator);

        return new CollectionsJsGenerationBuilder(propertyCheckerJsCodeGeneration, updateCollectionJsCodeGenerator);
    }

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

        var promiseJsGenerator = new PromiseJsGenerator();

        var storageElementsGenerator = new StoreElementsJsCodeGenerator(storageElementValueLineGenerator, storageElementCollectionLineGenerator, promiseJsGenerator, notImplementedCaseGuard);
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
