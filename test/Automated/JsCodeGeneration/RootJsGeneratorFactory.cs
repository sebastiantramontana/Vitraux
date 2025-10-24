using Vitraux.Execution.ViewModelNames;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.CustomJsGeneration;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.Test.JsCodeGeneration;

internal static class RootJsGeneratorFactory
{
    internal static IJsFullObjectNamesGenerator JsFullObjectNamesGenerator { get; private set; } = default!;
    internal static INotImplementedCaseGuard NotImplementedCaseGuard { get; private set; } = default!;
    internal static RootJsGenerator Create()
    {
        var getElementByIdAsArrayCall = new GetElementByIdAsArrayCall();
        var getStoredElementByIdAsArrayCall = new GetStoredElementByIdAsArrayCall();
        var getElementsByQuerySelectorCall = new GetElementsByQuerySelectorCall();
        var getStoredElementsByQuerySelectorCall = new GetStoredElementsByQuerySelectorCall();
        var getStoredTemplateCall = new GetStoredTemplateCall();
        var getFetchedElementCall = new GetFetchedElementCall();
        NotImplementedCaseGuard = new NotImplementedCaseGuard();

        var queryElementsGeneratorByStrategyContext = CreateQueryElementsJsCodeGeneratorByStrategyContext(getElementByIdAsArrayCall,
                                                                                                          getStoredElementByIdAsArrayCall,
                                                                                                          getElementsByQuerySelectorCall,
                                                                                                          getStoredElementsByQuerySelectorCall,
                                                                                                          getStoredTemplateCall,
                                                                                                          getFetchedElementCall,
                                                                                                          NotImplementedCaseGuard);
        var isValueValid = new IsValueValidCall();
        var codeFormatter = new CodeFormatter();
        var customJsJsGenerator = new CustomJsJsGenerator(new AtomicAutoNumberGenerator());
        var propertyCheckerJsCodeGeneration = new PropertyCheckerJsCodeGeneration(isValueValid, codeFormatter);

        var valueJsCodeGenerator = CreateValuesJsCodeGenerationBuilder(getElementsByQuerySelectorCall,
                                                                       propertyCheckerJsCodeGeneration,
                                                                       codeFormatter,
                                                                       NotImplementedCaseGuard,
                                                                       customJsJsGenerator);
        var uniqueSelectorsFilter = new UniqueSelectorsFilter();
        var jsObjectNamesGenerator = new JsObjectNamesGenerator(new AtomicAutoNumberGenerator());
        var jsElementObjectNamesGenerator = new JsElementObjectNamesGenerator(uniqueSelectorsFilter, jsObjectNamesGenerator, NotImplementedCaseGuard);

        var valueNamesGenerator = new ValueNamesGenerator(NotImplementedCaseGuard);
        var collectionNamesGenerator = new CollectionNamesGenerator();
        JsFullObjectNamesGenerator = new JsFullObjectNamesGenerator(valueNamesGenerator, collectionNamesGenerator, jsElementObjectNamesGenerator);

        var collectionsJsCodeGenerationBuilder = CreateCollectionsJsCodeGenerationBuilder(propertyCheckerJsCodeGeneration, codeFormatter, customJsJsGenerator);

        var promiseJsGenerator = new PromiseJsGenerator();
        var updateViewJsGenerator = new UpdateViewJsGenerator(promiseJsGenerator,
                                                              valueJsCodeGenerator,
                                                              collectionsJsCodeGenerationBuilder,
                                                              queryElementsGeneratorByStrategyContext);
        var initializeJsGeneratorContext = CreateInitializeJsGeneratorContext(getStoredElementByIdAsArrayCall,
                                                                              getStoredElementsByQuerySelectorCall,
                                                                              getStoredTemplateCall,
                                                                              getFetchedElementCall,
                                                                              NotImplementedCaseGuard,
                                                                              promiseJsGenerator);

        return new RootJsGenerator(initializeJsGeneratorContext, updateViewJsGenerator);
    }

    private static QueryElementsJsCodeGeneratorContext CreateQueryElementsJsCodeGeneratorByStrategyContext(
        IGetElementByIdAsArrayCall getElementByIdAsArrayCall,
        IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
        IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
        IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
        IGetStoredTemplateCall getStoredTemplateCall,
        IGetFetchedElementCall getFetchedElementCall,
        INotImplementedCaseGuard notImplementedCaseGuard)
    {
        var builder = new QueryElementsJsGenerator();

        var atStartGenerator = CreateAtStartGenerator(builder);
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

        return new QueryElementsJsCodeGeneratorContext(atStartGenerator, onDemandGenerator, onAlwaysGenerator);
    }

    private static CollectionsJsGenerationBuilder CreateCollectionsJsCodeGenerationBuilder(IPropertyCheckerJsCodeGeneration propertyCheckerJsCodeGeneration, ICodeFormatter codeFormatter, ICustomJsJsGenerator customJsJsGenerator)
    {
        var functionNameGenerator = new CollectionUpdateFunctionNameGenerator(new AtomicAutoNumberGenerator());
        var updateCollectionFunctionCallbackJsCodeGenerator = new UpdateCollectionFunctionCallbackJsCodeGenerator(functionNameGenerator, codeFormatter);
        var updateCollectionByPopulatingElementsCall = new UpdateCollectionByPopulatingElementsCall();
        var updateTableCall = new UpdateTableCall();
        var updateCollectionJsCodeGenerator = new UpdateCollectionJsCodeGenerator(updateTableCall, updateCollectionByPopulatingElementsCall, updateCollectionFunctionCallbackJsCodeGenerator, customJsJsGenerator, NotImplementedCaseGuard);

        return new CollectionsJsGenerationBuilder(propertyCheckerJsCodeGeneration, updateCollectionJsCodeGenerator);
    }

    private static ValuesJsCodeGenerationBuilder CreateValuesJsCodeGenerationBuilder(
        IGetElementsByQuerySelectorCall getElementsByQuerySelectorCall,
        IPropertyCheckerJsCodeGeneration propertyCheckerJsCodeGeneration,
        ICodeFormatter codeFormatter,
        INotImplementedCaseGuard notImplementedCaseGuard,
        ICustomJsJsGenerator customJsJsGenerator
        )
    {
        var setElementsAttributeCall = new SetElementsAttributeCall();
        var attributeCodeGenerator = new ElementPlaceAttributeJsGenerator(setElementsAttributeCall);

        var setElementsContentCall = new SetElementsContentCall();
        var contentCodeGenerator = new ElementPlaceContentJsGenerator(setElementsContentCall);

        var setElementsHtmlCall = new SetElementsHtmlCall();
        var htmlCodeGenerator = new ElementPlaceHtmlJsGenerator(setElementsHtmlCall);

        var toChildQueryFunctionCall = new ToChildQueryFunctionCall(getElementsByQuerySelectorCall);
        var updateByPopulatingElementsCall = new UpdateValueByInsertingElementsCall(codeFormatter);
        var updateChildElementsFunctionCall = new UpdateChildElementsFunctionCall(setElementsAttributeCall, setElementsContentCall, setElementsHtmlCall, notImplementedCaseGuard);

        var targetElementDirectUpdateValueJsCodeGenerator = new TargetElementsDirectUpdateValueJsGenerator(attributeCodeGenerator, contentCodeGenerator, htmlCodeGenerator, notImplementedCaseGuard);
        var targetByPopulatingElementsUpdateValueJsCodeGenerator = new TargetElementsUpdateValueInsertJsGenerator(updateByPopulatingElementsCall, toChildQueryFunctionCall, updateChildElementsFunctionCall, notImplementedCaseGuard);
        var viewModelKeyGenerator = new ViewModelKeyGenerator();
        var executeUpdateViewFunctionCall = new ExecuteUpdateViewFunctionCall();
        var targetElementsValueJsCodeGenerationBuilder = new TargetElementsValueJsGenerator(targetElementDirectUpdateValueJsCodeGenerator, targetByPopulatingElementsUpdateValueJsCodeGenerator, viewModelKeyGenerator, executeUpdateViewFunctionCall, customJsJsGenerator, notImplementedCaseGuard);

        return new ValuesJsCodeGenerationBuilder(propertyCheckerJsCodeGeneration, targetElementsValueJsCodeGenerationBuilder);
    }

    private static InitializeJsGeneratorContext CreateInitializeJsGeneratorContext(
        IGetStoredElementByIdAsArrayCall getStoredElementByIdAsArrayCall,
        IGetStoredElementsByQuerySelectorCall getStoredElementsByQuerySelectorCall,
        IGetStoredTemplateCall getStoredTemplateCall,
        IGetFetchedElementCall getFetchedElementCall,
        INotImplementedCaseGuard notImplementedCaseGuard,
        IPromiseJsGenerator promiseJsGenerator)
    {
        var onlyOnceAtStartInitializeJsGenerator = CreateOnlyOnceAtStartInitializeJsGenerator(getStoredElementByIdAsArrayCall, getStoredElementsByQuerySelectorCall, getStoredTemplateCall, getFetchedElementCall, notImplementedCaseGuard);
        var onlyOnceOnDemandInitializeJsGenerator = CreateOnlyOnceOnDemandInitializeJsGenerator(promiseJsGenerator);
        var alwaysInitializeJsGenerator = CreateAlwaysInitializeJsGenerator(promiseJsGenerator);

        return new InitializeJsGeneratorContext(onlyOnceAtStartInitializeJsGenerator, onlyOnceOnDemandInitializeJsGenerator, alwaysInitializeJsGenerator, notImplementedCaseGuard);
    }

    private static OnlyOnceAtStartInitializeJsGenerator CreateOnlyOnceAtStartInitializeJsGenerator(
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

        var jsLineGeneratorCollectionByTemplate = new StorageElementCollectionJsLineGeneratorByTemplate(storageElementJsLineGeneratorByTemplate, notImplementedCaseGuard);
        var jsLineGeneratorCollectionByUri = new StorageElementCollectionJsLineGeneratorByUri(storageElementJsLineGeneratorByUri, notImplementedCaseGuard);
        var storageElementCollectionLineGenerator = new StorageElementCollectionJsLineGenerator(jsLineGeneratorCollectionByTemplate, jsLineGeneratorCollectionByUri, notImplementedCaseGuard);

        var promiseJsGenerator = new PromiseJsGenerator();

        var storageElementValueLineGenerator = new StorageElementValueJsLineGenerator(storageElementJsLineGeneratorElementsById,
                                                                                      storageElementJsLineGeneratorElementsByQuery,
                                                                                      storageElementJsLineGeneratorInsertElementsByTemplate,
                                                                                      storageElementJsLineGeneratorInsertElementsByUri,
                                                                                      notImplementedCaseGuard);

        return new OnlyOnceAtStartInitializeJsGenerator(storageElementValueLineGenerator, storageElementCollectionLineGenerator, promiseJsGenerator, notImplementedCaseGuard);
    }

    private static OnlyOnceOnDemandInitializeJsGenerator CreateOnlyOnceOnDemandInitializeJsGenerator(IPromiseJsGenerator promiseJsGenerator)
        => new(promiseJsGenerator);

    private static AlwaysInitializeJsGenerator CreateAlwaysInitializeJsGenerator(IPromiseJsGenerator promiseJsGenerator)
        => new(promiseJsGenerator);

    private static QueryElementsOnlyOnceAtStartJsGenerator CreateAtStartGenerator(IQueryElementsJsGenerator builder)
    {
        var atStartDeclaringGenerator = new QueryElementsDeclaringOnlyOnceAtStartJsGenerator();
        return new QueryElementsOnlyOnceAtStartJsGenerator(builder, atStartDeclaringGenerator);
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
