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
                                            """;

        const string expectedCodeOnDemand = """
                                            const elements0 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'elements0');
                                            const elements1 = globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'elements1');
                                            const elements1_appendTo = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-parent', 'elements1_appendTo');
                                            const elements2 = globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.petwoner-address > div', 'elements2');
                                            const elements3 = globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'elements3');
                                            """;

        const string expectedCodeAlways = """
                                          const elements0 = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-name');
                                          const elements1 = globalThis.vitraux.storedElements.getTemplate('petowner-address-template');
                                          const elements1_appendTo = globalThis.vitraux.storedElements.getElementByIdAsArray('petowner-parent');
                                          const elements2 = globalThis.vitraux.storedElements.getElementsByQuerySelector(document, '.petwoner-address > div');
                                          const elements3 = globalThis.vitraux.storedElements.getElementByIdAsArray('pets-table');
                                          """;

        const string expectedExecutedCodeForAtStart = """
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-name', 'elements0');
                                                    globalThis.vitraux.storedElements.getStoredTemplate('petowner-address-template', 'elements1');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('petowner-parent', 'elements1_appendTo');
                                                    globalThis.vitraux.storedElements.getStoredElementsByQuerySelector(document, 'document', '.petwoner-address > div', 'elements2');
                                                    globalThis.vitraux.storedElements.getStoredElementByIdAsArray('pets-table', 'elements3');
                                                    """;

        const string expectedCodeForValues = """
                                            if(vm.value0) {
                                                globalThis.vitraux.updating.setElementsContent(elements0, vm.value0);
                                            }

                                            if(vm.value1) {
                                                globalThis.vitraux.updating.UpdateByTemplate(
                                                elements1[0],
                                                elements1_appendTo,
                                                (templateContent) => globalThis.vitraux.storedElements.getElementsByQuerySelector(templateContent, '.petowner-address-target'),
                                                (targetTemplateChildElements) => globalThis.vitraux.updating.setElementsContent(targetTemplateChildElements, vm.value1));

                                                globalThis.vitraux.updating.setElementsAttribute(elements2, 'data-petowner-address', vm.value1);
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

            var queryElementsGeneratorByStrategyFactory = CreateQueryElementsJsCodeGeneratorByStrategyFactory(jsCodeExecutor, getElementByIdAsArrayCall, getElementsByQuerySelectorCall);
            var elementNamesGenerator = new ElementNamesGenerator();
            var valueNamesGenerator = new ValueNamesGenerator();
            var valueJsCodeGenerator = CreateValuesJsCodeGenerator(getElementsByQuerySelectorCall);

            return new JsGenerator<PetOwner>(queryElementsGeneratorByStrategyFactory, elementNamesGenerator, valueNamesGenerator, valueJsCodeGenerator);
        }

        private static QueryElementsJsCodeGeneratorByStrategyFactory CreateQueryElementsJsCodeGeneratorByStrategyFactory(IJsCodeExecutor jsCodeExecutor, IGetElementByIdAsArrayCall getElementByIdAsArrayCall, IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall)
        {
            var builder = new QueryElementsJsCodeBuilder();
            var getStoredElementByIdAsArrayCall = new GetStoredElementByIdAsArrayCall();
            var getStoredElementsByQuerySelectorCall = new GetStoredElementsByQuerySelectorCall();
            var queryAppendToElementsDeclaringByTemplateJsCodeGenerator = new QueryAppendToElementsDeclaringByTemplateJsCodeGenerator();
            var queryTemplateCallingJsBuiltInFunctionCodeGenerator = new QueryTemplateCallingJsBuiltInFunctionCodeGenerator(queryAppendToElementsDeclaringByTemplateJsCodeGenerator);

            var atStartGenerator = CreateAtStartGenerator(builder, jsCodeExecutor, getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall);
            var onDemandGenerator = CreateOnDemandGenerator(builder, getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator);
            var onAlwaysGenerator = CreateAlwaysGenerator(builder, getElementByIdAsArrayCall, getElementsByQuerySelectorCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator);

            return new QueryElementsJsCodeGeneratorByStrategyFactory(atStartGenerator, onDemandGenerator, onAlwaysGenerator);
        }

        private static ValuesJsCodeGenerator CreateValuesJsCodeGenerator(IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall)
        {
            var setElementsAttributeCall = new SetElementsAttributeCall();
            var attributeCodeGenerator = new ElementPlaceAttributeJsCodeGenerator(setElementsAttributeCall);

            var setElementsContentCall = new SetElementsContentCall();
            var contentCodeGenerator = new ElementPlaceContentJsCodeGenerator(setElementsContentCall);

            var codeFormatting = new CodeFormatting();

            var updateByTemplateCall = new UpdateByTemplateCall(codeFormatting);

            var targetElementDirectUpdateJsCodeGeneration = new TargetElementDirectUpdateValueJsCodeGenerator(attributeCodeGenerator, contentCodeGenerator, codeFormatting);
            var targetElementTemplateUpdateJsCodeGeneration = new TargetElementTemplateUpdateValueJsCodeGenerator(updateByTemplateCall, getElementsByQuerySelectorCall, setElementsAttributeCall, setElementsContentCall, codeFormatting);
            var targetElementsJsCodeGenerationBuilder = new TargetElementsJsCodeGenerationBuilder(targetElementDirectUpdateJsCodeGeneration, targetElementTemplateUpdateJsCodeGeneration);
            var valueCheckJsCodeGeneration = new ValueCheckJsCodeGeneration();
            var valuesJsCodeGenerationBuilder = new ValuesJsCodeGenerationBuilder(valueCheckJsCodeGeneration, targetElementsJsCodeGenerationBuilder);
            return new ValuesJsCodeGenerator(valuesJsCodeGenerationBuilder);
        }

        private static QueryElementsOnlyOnceAtStartJsCodeGenerator CreateAtStartGenerator(IQueryElementsJsCodeBuilder builder, IJsCodeExecutor jsCodeExecutor, IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall, IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall)
        {
            var generatorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
            var generatorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
            var getStoredTemplateCall = new GetStoredTemplateCall();
            var storageElementJsLineGeneratorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
            var storageElementJsLineGeneratorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
            var storageFromTemplateElementJsLineGenerator = new StorageFromTemplateElementJsLineGenerator(storageElementJsLineGeneratorById, storageElementJsLineGeneratorByQuerySelector);
            var generatorByTemplate = new StorageElementJsLineGeneratorByTemplate(getStoredTemplateCall, storageFromTemplateElementJsLineGenerator);
            var storageElementLineGenerator = new StorageElementJsLineGenerator(generatorById, generatorByQuerySelector, generatorByTemplate);
            var storageElementsBuilder = new StoreElementsJsCodeBuilder(storageElementLineGenerator);
            var initializer = new QueryElementsOnlyOnceAtStartup(storageElementsBuilder, jsCodeExecutor);
            var atStartDeclaringGenerator = new QueryElementsDeclaringOnlyOnceAtStartJsCodeGenerator();

            return new QueryElementsOnlyOnceAtStartJsCodeGenerator(builder, atStartDeclaringGenerator, initializer);
        }

        private static QueryElementsOnlyOnceOnDemandJsCodeGenerator CreateOnDemandGenerator(
            IQueryElementsJsCodeBuilder builder,
            IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
            IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
            IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryTemplateCallingJsBuiltInFunctionCodeGenerator)
        {
            var declaringOnlyOncenDemandByIdGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByIdJsCodeGenerator(getStoredElementByIdAsArrayCall);
            var declaringOnlyOnceOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsCodeGenerator(getStoredElementsByQuerySelectorCall);
            var getStoredTemplateCall = new GetStoredTemplateCall();
            var jsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory = new JsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory(declaringOnlyOncenDemandByIdGenerator, declaringOnlyOnceOnDemandByQuerySelectorGenerator);
            var declaringOnlyOnceOnDemandByTemplateGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator(getStoredTemplateCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory);
            var onDemandGeneratorFactory = new JsQueryElementsOnlyOnceOnDemandGeneratorFactory(declaringOnlyOncenDemandByIdGenerator, declaringOnlyOnceOnDemandByQuerySelectorGenerator, declaringOnlyOnceOnDemandByTemplateGenerator);
            var declaringOnlyOnceOnDemandGenerator = new QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(onDemandGeneratorFactory);

            return new QueryElementsOnlyOnceOnDemandJsCodeGenerator(builder, declaringOnlyOnceOnDemandGenerator);
        }

        private static QueryElementsAlwaysJsCodeGenerator CreateAlwaysGenerator(
            IQueryElementsJsCodeBuilder builder,
            IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
            IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
            IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryTemplateCallingJsBuiltInFunctionCodeGenerator)
        {
            var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsCodeGenerator(getElementByIdAsArrayCall);
            var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsCodeGenerator(getElementsByQuerySelectorCall);
            var getTemplateCall = new GetTemplateCall();
            var jsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory = new JsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator);
            var declaringAlwaysByTemplateGenerator = new QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator(getTemplateCall, queryTemplateCallingJsBuiltInFunctionCodeGenerator, jsQueryFromTemplateElementsDeclaringAlwaysGeneratorFactory);
            var alwaysGeneratorFactory = new JsQueryElementsDeclaringAlwaysGeneratorFactory(declaringAlwaysByIdGenerator, declaringAlwaysByQuerySelectorGenerator, declaringAlwaysByTemplateGenerator);
            var declaringAlwaysGenerator = new QueryElementsDeclaringAlwaysCodeGenerator(alwaysGeneratorFactory);

            return new QueryElementsAlwaysJsCodeGenerator(declaringAlwaysGenerator, builder);
        }
    }
}
