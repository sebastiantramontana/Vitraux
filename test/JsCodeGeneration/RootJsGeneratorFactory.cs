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

namespace Vitraux.Test.JsCodeGeneration;
internal static class RootJsGeneratorFactory
{
    internal static RootJsGenerator Create(IJsCodeExecutor jsCodeExecutor)
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
        var valueNamesGenerator = new ValueNamesGenerator(notImplementedCaseGuard);
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
