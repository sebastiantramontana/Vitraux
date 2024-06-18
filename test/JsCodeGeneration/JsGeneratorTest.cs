using Moq;
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
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Test.Example;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;

namespace Vitraux.Test.JsCodeGeneration
{
    [TestFixture]
    public class JsGeneratorTest
    {
        const string expectedCodeAtStart = """
                                            const elements0 = globalThis.vitraux.storedElements.elements.document.elements0;
                                            const elements1 = globalThis.vitraux.storedElements.elements.document.elements1;
                                            const elements1_appendTo = globalThis.vitraux.storedElements.elements.document.elements1_appendTo;
                                            const elements2 = globalThis.vitraux.storedElements.elements.document.elements2;
                                            const elements3 = globalThis.vitraux.storedElements.elements.document.elements3;
                                            const elements3_appendTo = globalThis.vitraux.storedElements.elements.document.elements3_appendTo;
                                            const elements4 = globalThis.vitraux.storedElements.elements.document.elements4;
                                            const elements5 = globalThis.vitraux.storedElements.elements.document.elements5;
                                            """;

        const string expectedCodeOnDemand = """
                                            const elements0 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'elements0');
                                            const elements1 = globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'elements1');
                                            const elements1_appendTo = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-parent', 'elements1_appendTo');
                                            const elements2 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.petwoner-address > div', 'elements2');
                                            const elements3 = await globalThis.vitraux.storedElements.getFetchedElement('https://mysite.com/htmlparts/phoneblock', 'elements3');
                                            const elements3_appendTo = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.petowner-phonenumber', 'elements3_appendTo');
                                            const elements4 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', 'elements4');
                                            const elements5 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'elements5');
                                            """;

        const string expectedCodeAlways = """
                                          const elements0 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-name');
                                          const elements1 = globalThis.vitraux.storedElements.getTemplate('petowner-address-template');
                                          const elements1_appendTo = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-parent');
                                          const elements2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petwoner-address > div');
                                          const elements3 = await globalThis.vitraux.storedElements.fetchElement('https://mysite.com/htmlparts/phoneblock');
                                          const elements3_appendTo = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petowner-phonenumber');
                                          const elements4 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-phonenumber-id');
                                          const elements5 = globalThis.vitraux.storedElements.getElementByIdAsArray('pets-table');
                                          """;

        const string expectedExecutedCodeForAtStart = """
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'elements0');
                                                    globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'elements1');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-parent', 'elements1_appendTo');
                                                    globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.petwoner-address > div', 'elements2');
                                                    await globalThis.vitraux.storedElements.getFetchedElement('https://mysite.com/htmlparts/phoneblock', 'elements3');
                                                    globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.petowner-phonenumber', 'elements3_appendTo');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-phonenumber-id', 'elements4');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'elements5');
                                                    """;

        const string expectedCodeForValues = """
                                            if(vm.value0) {
                                                globalThis.vitraux.updating.setElementsContent(elements0, vm.value0);
                                            }

                                            if(vm.value1) {
                                                globalThis.vitraux.updating.UpdateByPopulatingElements(
                                                elements1,
                                                elements1_appendTo,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-address-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.setElementsContent(targetChildElements, vm.value1));

                                                globalThis.vitraux.updating.setElementsAttribute(elements2, 'data-petowner-address', vm.value1);
                                            }

                                            if(vm.value2) {
                                                globalThis.vitraux.updating.UpdateByPopulatingElements(
                                                elements3,
                                                elements3_appendTo,
                                                (content) => globalThis.vitraux.storedElements.getElementsByQuerySelector(content, '.petowner-phonenumber-target'),
                                                (targetChildElements) => globalThis.vitraux.updating.setElementsAttribute(targetChildElements, 'data-phonenumber', vm.value2));

                                                globalThis.vitraux.updating.setElementsContent(elements4, vm.value2);
                                            }
                                            """;

        [Test]
        [TestCase(QueryElementStrategy.OnlyOnceAtStart, expectedCodeAtStart)]
        [TestCase(QueryElementStrategy.OnlyOnceOnDemand, expectedCodeOnDemand)]
        [TestCase(QueryElementStrategy.Always, expectedCodeAlways)]
        public void GenerateCodeTest(QueryElementStrategy queryElementStrategy, string expectedQueryElementsCode)
        {
            var executorMock = new Mock<IJsCodeExecutor>();

            var sut = CreateSut(executorMock.Object);
            var personaConfig = new PetOwnerConfiguration(new DataUriConverter());

            var modelMapper = new ModelMapperRoot<PetOwner>();
            var data = personaConfig.ConfigureMapping(modelMapper);

            var actualCode = sut.GenerateJsCode(data, new ConfigurationBehavior(queryElementStrategy, false));

            var expectedCode = expectedQueryElementsCode + Environment.NewLine + Environment.NewLine + expectedCodeForValues;

            Assert.That(actualCode, Is.EqualTo(expectedCode));

            if (queryElementStrategy == QueryElementStrategy.OnlyOnceAtStart)
                executorMock.Verify(e => e.Excute(expectedExecutedCodeForAtStart), Times.Once);
        }

        [Test]
        public void SampleToTestGeneratedJsCode()
        {
            //Arrange
            var html = """
                <!DOCTYPE html>

                <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                <head>
                    <meta charset="utf-8" />
                    <title>Some Title</title>
                </head>
                <body>
                <div id="test_id">text to change</div>
                </body>
                </html>
                """;

            var options = new ChromeOptions();
            // Avoid opening a Chrome window browser: The test runs in memory
            options.AddArguments("--disable-crash-reporter", "--enable-gpu", "--force-headless-for-tests", "--headless");

            //Also: FireFoxDriver, EdgeDriver and SafariDriver
            IWebDriver driver = new ChromeDriver(options);

            //Load a page with the desired html
            driver.Navigate().GoToUrl("about:blank"); //First blank page...
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"document.write(arguments[0]);", html); //then, write the html

            //Act
            js.ExecuteScript("document.getElementById('test_id').innerHTML = 'text changed'");

            //Assert
            var element = driver.FindElement(By.Id("test_id"));
            Assert.That(element.Text, Is.EqualTo("text changed"));
        }

        private static JsGenerator<PetOwner> CreateSut(IJsCodeExecutor jsCodeExecutor)
        {
            var getElementByIdAsArrayCall = new GetElementByIdAsArrayCall();
            var getElementsByQuerySelectorCall = new GetElementsByQuerySelectorCall();

            var queryElementsGeneratorByStrategyContext = CreateQueryElementsJsCodeGeneratorByStrategyContext(jsCodeExecutor, getElementByIdAsArrayCall, getElementsByQuerySelectorCall);
            var elementNamesGenerator = new ElementNamesGenerator();
            var valueNamesGenerator = new ValueNamesGenerator();
            var valueJsCodeGenerator = CreateValuesJsCodeGenerationBuilder(getElementsByQuerySelectorCall);

            return new JsGenerator<PetOwner>(queryElementsGeneratorByStrategyContext, elementNamesGenerator, valueNamesGenerator, valueJsCodeGenerator);
        }

        private static QueryElementsJsCodeGeneratorByStrategyContext CreateQueryElementsJsCodeGeneratorByStrategyContext(IJsCodeExecutor jsCodeExecutor, IGetElementByIdAsArrayCall getElementByIdAsArrayCall, IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall)
        {
            var builder = new QueryElementsJsCodeBuilder();
            var getStoredElementByIdAsArrayCall = new GetStoredElementByIdAsArrayCall();
            var getStoredElementsByQuerySelectorCall = new GetStoredElementsByQuerySelectorCall();
            var queryAppendToElementsDeclaringByTemplateJsCodeGenerator = new QueryAppendToElementsDeclaringByPopulatingJsCodeGenerator();
            var queryTemplateCallingJsBuiltInFunctionCodeGenerator = new QueryPopulatingCallingJsBuiltInFunctionCodeGenerator(queryAppendToElementsDeclaringByTemplateJsCodeGenerator);

            var atStartGenerator = CreateAtStartGenerator(builder, jsCodeExecutor, getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall);
            var onDemandGenerator = CreateOnDemandGenerator(builder, getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator);
            var onAlwaysGenerator = CreateAlwaysGenerator(builder, getElementByIdAsArrayCall, getElementsByQuerySelectorCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator);

            return new QueryElementsJsCodeGeneratorByStrategyContext(atStartGenerator, onDemandGenerator, onAlwaysGenerator);
        }

        private static IValuesJsCodeGenerationBuilder CreateValuesJsCodeGenerationBuilder(IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall)
        {
            var setElementsAttributeCall = new SetElementsAttributeCall();
            var attributeCodeGenerator = new ElementPlaceAttributeJsCodeGenerator(setElementsAttributeCall);

            var setElementsContentCall = new SetElementsContentCall();
            var contentCodeGenerator = new ElementPlaceContentJsCodeGenerator(setElementsContentCall);

            var codeFormatting = new CodeFormatting();
            var toChildQueryFunctionCall = new ToChildQueryFunctionCall(getElementsByQuerySelectorCall);
            var updateByPopulatingElementsCall = new UpdateByPopulatingElementsCall(codeFormatting);
            var updateChildElementsFunctionCall = new UpdateChildElementsFunctionCall(setElementsAttributeCall, setElementsContentCall);

            var targetElementDirectUpdateJsCodeGeneration = new TargetElementDirectUpdateValueJsCodeGenerator(attributeCodeGenerator, contentCodeGenerator, codeFormatting);
            var targetByPopulatingElementsUpdateValueJsCodeGenerator = new TargetByPopulatingElementsUpdateValueJsCodeGenerator(updateByPopulatingElementsCall, toChildQueryFunctionCall, updateChildElementsFunctionCall, codeFormatting);
            var targetElementsJsCodeGenerationBuilder = new TargetElementsJsCodeGenerationBuilder(targetElementDirectUpdateJsCodeGeneration, targetByPopulatingElementsUpdateValueJsCodeGenerator);
            var valueCheckJsCodeGeneration = new ValueCheckJsCodeGeneration();
            return new ValuesJsCodeGenerationBuilder(valueCheckJsCodeGeneration, targetElementsJsCodeGenerationBuilder);
        }

        private static QueryElementsOnlyOnceAtStartJsCodeGenerator CreateAtStartGenerator(IQueryElementsJsCodeBuilder builder, IJsCodeExecutor jsCodeExecutor, IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall, IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall)
        {
            var generatorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
            var generatorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
            var getStoredTemplateCall = new GetStoredTemplateCall();
            var getFetchedElementCall = new GetFetchedElementCall();
            var storageElementJsLineGeneratorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
            var storageElementJsLineGeneratorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
            var storagePopulatingAppendToElementJsLineGenerator = new StoragePopulatingAppendToElementJsLineGenerator(storageElementJsLineGeneratorById, storageElementJsLineGeneratorByQuerySelector);
            var storagePopulatingElementJsLineGenerator = new StoragePopulatingElementJsLineGenerator(storagePopulatingAppendToElementJsLineGenerator);
            var generatorByTemplate = new StorageElementJsLineGeneratorByTemplate(getStoredTemplateCall, storagePopulatingElementJsLineGenerator);
            var generatorByFetch = new StorageElementJsLineGeneratorByFetch(getFetchedElementCall, storagePopulatingElementJsLineGenerator);
            var storageElementLineGenerator = new StorageElementJsLineGenerator(generatorById, generatorByQuerySelector, generatorByTemplate, generatorByFetch);
            var storageElementsBuilder = new StoreElementsJsCodeBuilder(storageElementLineGenerator);
            var initializer = new QueryElementsOnlyOnceAtStartup(storageElementsBuilder, jsCodeExecutor);
            var atStartDeclaringGenerator = new QueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator();

            return new QueryElementsOnlyOnceAtStartJsCodeGenerator(builder, atStartDeclaringGenerator, initializer);
        }

        private static QueryElementsOnlyOnceOnDemandJsCodeGenerator CreateOnDemandGenerator(
            IQueryElementsJsCodeBuilder builder,
            IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
            IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
            IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryTemplateCallingJsBuiltInFunctionCodeGenerator)
        {
            var declaringOnlyOncenDemandByIdGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator(getStoredElementByIdAsArrayCall);
            var declaringOnlyOnceOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator(getStoredElementsByQuerySelectorCall);
            var getStoredTemplateCall = new GetStoredTemplateCall();
            var getFetchedElementCall = new GetFetchedElementCall();
            var jsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorContext = new JsQueryPopulatingElementsDeclaringOnlyOnceOnDemandGeneratorContext(declaringOnlyOncenDemandByIdGenerator, declaringOnlyOnceOnDemandByQuerySelectorGenerator);
            var declaringOnlyOnceOnDemandByTemplateGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator(getStoredTemplateCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorContext);
            var declaringOnlyOnceOnDemandByFetchGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByFetchJsCodeGenerator(getFetchedElementCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorContext);
            var onDemandGeneratorContext = new JsQueryElementsOnlyOnceOnDemandGeneratorContext(declaringOnlyOncenDemandByIdGenerator, declaringOnlyOnceOnDemandByQuerySelectorGenerator, declaringOnlyOnceOnDemandByTemplateGenerator, declaringOnlyOnceOnDemandByFetchGenerator);
            var declaringOnlyOnceOnDemandGenerator = new QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(onDemandGeneratorContext);

            return new QueryElementsOnlyOnceOnDemandJsCodeGenerator(builder, declaringOnlyOnceOnDemandGenerator);
        }

        private static QueryElementsAlwaysJsCodeGenerator CreateAlwaysGenerator(
            IQueryElementsJsCodeBuilder builder,
            IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
            IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
            IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryTemplateCallingJsBuiltInFunctionCodeGenerator)
        {
            var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsCodeGenerator(getElementByIdAsArrayCall);
            var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator(getElementsByQuerySelectorCall);
            var getTemplateCall = new GetTemplateCall();
            var fetchElementCall = new FetchElementCall();
            var jsQueryFromTemplateElementsDeclaringAlwaysGeneratorContext = new JsQueryPopulatingElementsDeclaringAlwaysGeneratorContext(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator);
            var declaringAlwaysByTemplateGenerator = new QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator(getTemplateCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringAlwaysGeneratorContext);
            var declaringAlwaysByFetchGenerator = new QueryElementsDeclaringAlwaysByFetchJsCodeGenerator(fetchElementCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringAlwaysGeneratorContext);
            var alwaysGeneratorContext = new JsQueryElementsDeclaringAlwaysGeneratorContext(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator, declaringAlwaysByTemplateGenerator, declaringAlwaysByFetchGenerator);
            var declaringAlwaysGenerator = new QueryElementsDeclaringAlwaysCodeGenerator(alwaysGeneratorContext);

            return new QueryElementsAlwaysJsCodeGenerator(declaringAlwaysGenerator, builder);
        }
    }
}
