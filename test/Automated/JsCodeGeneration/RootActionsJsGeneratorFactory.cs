using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.Actions;
using Vitraux.JsCodeGeneration.Actions.Parameters;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions;
using Vitraux.JsCodeGeneration.BuiltInCalling.Actions.Parametrizables;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

namespace Vitraux.Test.JsCodeGeneration;

internal static class RootActionsJsGeneratorFactory
{
    public static NotImplementedCaseGuard NotImplementedCaseGuard { get; } = new NotImplementedCaseGuard();

    internal static RootActionsJsGenerator Create()
    {
        // helpers
        var codeFormatter = new CodeFormatter();
        var atomicAutoNumberGenerator = new AtomicAutoNumberGenerator();

        // Built-in calls (non parametrizable)
        var getElementByIdAsArrayCall = new GetElementByIdAsArrayCall();
        var getElementsByQuerySelectorCall = new GetElementsByQuerySelectorCall();
        var getTemplateCall = new GetTemplateCall();
        var fetchElementCall = new FetchElementCall();
        var getStoredElementByIdAsArrayCall = new GetStoredElementByIdAsArrayCall();
        var getStoredElementsByQuerySelectorCall = new GetStoredElementsByQuerySelectorCall();
        var getStoredTemplateCall = new GetStoredTemplateCall();
        var getFetchedElementCall = new GetFetchedElementCall();

        // Query elements builder
        var queryElementsBuilder = new QueryElementsJsGenerator(codeFormatter);

        // ALWAYS strategy generators
        var declaringAlwaysByIdGenerator = new QueryElementsDeclaringAlwaysByIdJsGenerator(getElementByIdAsArrayCall, NotImplementedCaseGuard);
        var declaringAlwaysByQuerySelectorGenerator = new QueryElementsDeclaringAlwaysByQuerySelectorJsGenerator(getElementsByQuerySelectorCall, NotImplementedCaseGuard);
        var declaringAlwaysValueByTemplateGenerator = new QueryElementsDeclaringAlwaysValueByTemplateJsGenerator(getTemplateCall, NotImplementedCaseGuard);
        var declaringAlwaysValueByUriGenerator = new QueryElementsDeclaringAlwaysValueByUriJsGenerator(fetchElementCall, NotImplementedCaseGuard);
        var declaringAlwaysCollectionByTemplateGenerator = new QueryElementsDeclaringAlwaysCollectionByTemplateJsGenerator(getTemplateCall, NotImplementedCaseGuard);
        var declaringAlwaysCollectionByUriGenerator = new QueryElementsDeclaringAlwaysCollectionByUriJsGenerator(fetchElementCall, NotImplementedCaseGuard);
        var alwaysContext = new JsQueryElementsDeclaringAlwaysGeneratorContext(
            declaringAlwaysByIdGenerator,
            declaringAlwaysByQuerySelectorGenerator,
            declaringAlwaysValueByTemplateGenerator,
            declaringAlwaysValueByUriGenerator,
            declaringAlwaysCollectionByTemplateGenerator,
            declaringAlwaysCollectionByUriGenerator,
            NotImplementedCaseGuard);
        var declaringAlwaysGenerator = new QueryElementsDeclaringAlwaysCodeGenerator(alwaysContext);
        var alwaysGenerator = new QueryElementsAlwaysJsCodeGenerator(declaringAlwaysGenerator, queryElementsBuilder);

        // ONLY ONCE AT START strategy generators
        var declaringAtStartGenerator = new QueryElementsDeclaringOnlyOnceAtStartJsGenerator();
        var atStartGenerator = new QueryElementsOnlyOnceAtStartJsGenerator(queryElementsBuilder, declaringAtStartGenerator);

        // ONLY ONCE ON DEMAND strategy generators
        var declaringOnDemandByIdGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByIdJsGenerator(getStoredElementByIdAsArrayCall, NotImplementedCaseGuard);
        var declaringOnDemandByQuerySelectorGenerator = new QueryElementsDeclaringOnlyOnceOnDemandByQuerySelectorJsGenerator(getStoredElementsByQuerySelectorCall, NotImplementedCaseGuard);
        var declaringOnDemandValueByTemplateGenerator = new QueryElementsDeclaringOnlyOnceOnDemandValueByTemplateJsGenerator(getStoredTemplateCall, NotImplementedCaseGuard);
        var declaringOnDemandValueByUriGenerator = new QueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator(getFetchedElementCall, NotImplementedCaseGuard);
        var declaringOnDemandCollectionByTemplateGenerator = new QueryElementsDeclaringOnlyOnceOnDemandCollectionByTemplateJsGenerator(getStoredTemplateCall, NotImplementedCaseGuard);
        var declaringOnDemandCollectionByUriGenerator = new QueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator(getFetchedElementCall, NotImplementedCaseGuard);
        var onDemandContext = new JsQueryElementsOnlyOnceOnDemandGeneratorContext(
            declaringOnDemandByIdGenerator,
            declaringOnDemandByQuerySelectorGenerator,
            declaringOnDemandValueByTemplateGenerator,
            declaringOnDemandValueByUriGenerator,
            declaringOnDemandCollectionByTemplateGenerator,
            declaringOnDemandCollectionByUriGenerator,
            NotImplementedCaseGuard);
        var declaringOnDemandGenerator = new QueryElementsDeclaringOnlyOnceOnDemandJsCodeGenerator(onDemandContext);
        var onDemandGenerator = new QueryElementsOnlyOnceOnDemandJsCodeGenerator(queryElementsBuilder, declaringOnDemandGenerator);

        // Query elements context (for parametrizable actions callback queries)
        var queryElementsContext = new QueryElementsJsCodeGeneratorContext(atStartGenerator, onDemandGenerator, alwaysGenerator);

        // Input elements query generator (uses ALWAYS strategy only)
        var rootActionInputElementsQueryJsGenerator = new RootActionInputElementsQueryJsGenerator(alwaysGenerator);

        // Filters for input & parameters
        var actionInputJsElementObjectNamesFilter = new ActionInputJsElementObjectNamesFilter();
        var actionParameterJsElementObjectNamesFilter = new ActionParameterJsElementObjectNamesFilter();

        // Storage line generators for parameter init (OnlyOnceAtStart)
        var storageElementJsLineGeneratorById = new StorageElementJsLineGeneratorById(getStoredElementByIdAsArrayCall);
        var storageElementJsLineGeneratorByQuerySelector = new StorageElementJsLineGeneratorByQuerySelector(getStoredElementsByQuerySelectorCall);
        var storageElementJsLineGeneratorElementsById = new StorageElementJsLineGeneratorElementsById(storageElementJsLineGeneratorById, NotImplementedCaseGuard);
        var storageElementJsLineGeneratorElementsByQuery = new StorageElementJsLineGeneratorElementsByQuery(storageElementJsLineGeneratorByQuerySelector, NotImplementedCaseGuard);
        var storageElementActionJsLineGenerator = new StorageElementActionJsLineGenerator(storageElementJsLineGeneratorElementsById, storageElementJsLineGeneratorElementsByQuery, NotImplementedCaseGuard);
        var rootActionOnlyOnceAtStartParameterInitQueryJsGenerator = new RootActionOnlyOnceAtStartParameterInitQueryJsGenerator(storageElementActionJsLineGenerator);

        // Registration calls (non parametrizable and parametrizable)
        var registerActionAsyncCallGenerator = new RegisterActionCallGenerator();
        var registerActionAsyncCall = new RegisterActionAsyncCall(registerActionAsyncCallGenerator);
        var registerActionSyncCall = new RegisterActionSyncCall(registerActionAsyncCallGenerator);
        var rootActionInputEventsRegistrationJsGenerator = new RootActionInputEventsRegistrationJsGenerator(registerActionAsyncCall, registerActionSyncCall);
        var registerParametrizableActionCallGenerator = new RegisterParametrizableActionCallGenerator();
        var registerParametrizableActionAsyncCall = new RegisterParametrizableActionAsyncCall(registerParametrizableActionCallGenerator);
        var registerParametrizableActionSyncCall = new RegisterParametrizableActionSyncCall(registerParametrizableActionCallGenerator);
        var rootParametrizableActionInputEventsRegistrationJsGenerator = new RootParametrizableActionInputEventsRegistrationJsGenerator(registerParametrizableActionAsyncCall, registerParametrizableActionSyncCall);

        // Action parameters value getters
        var getInputValueCall = new GetInputValueCall();
        var getElementsContentCall = new GetElementsContentCall();
        var getElementsAttributeCall = new GetElementsAttributeCall();
        var actionParameterGettingValueCallGenerator = new ActionParameterGettingValueCallGenerator(getInputValueCall, getElementsContentCall, getElementsAttributeCall, NotImplementedCaseGuard);

        // Callback args object generators
        var rootActionParametersCallbackArgumentsJsObjectGenerator = new RootActionParametersCallbackArgumentsJsObjectGenerator(codeFormatter, actionParameterGettingValueCallGenerator);
        var rootActionParametersCallbackConstArgsJsGenerator = new RootActionParametersCallbackConstArgsJsGenerator(rootActionParametersCallbackArgumentsJsObjectGenerator, codeFormatter);
        var rootActionParametersCallbackInputValueParameterJsGenerator = new RootActionParametersCallbackInputValueParameterJsGenerator(codeFormatter);
        var rootActionParametersCallbackReturnArgsJsGenerator = new RootActionParametersCallbackReturnArgsJsGenerator(codeFormatter);
        var rootActionParametersCallbackQueryElementsJsGenerator = new RootActionParametersCallbackQueryElementsJsGenerator(queryElementsContext);
        var rootActionParametersCallbackBodyJsGenerator = new RootActionParametersCallbackBodyJsGenerator(
            rootActionParametersCallbackQueryElementsJsGenerator,
            rootActionParametersCallbackConstArgsJsGenerator,
            rootActionParametersCallbackInputValueParameterJsGenerator,
            rootActionParametersCallbackReturnArgsJsGenerator);
        var rootActionParametersCallbackJsGenerator = new RootActionParametersCallbackJsGenerator(rootActionParametersCallbackBodyJsGenerator);

        // Callback function name generator
        var actionParametersCallbackFunctionNameGenerator = new ActionParametersCallbackFunctionNameGenerator(atomicAutoNumberGenerator);

        // Single action generators (parameterless & parametrizable)
        var rootSingleParameterlessActionJsGenerator = new RootSingleParameterlessActionJsGenerator(
            rootActionInputElementsQueryJsGenerator,
            actionInputJsElementObjectNamesFilter,
            rootActionInputEventsRegistrationJsGenerator);

        var rootSingleParametrizableActionJsGenerator = new RootSingleParametrizableActionJsGenerator(
            rootActionInputElementsQueryJsGenerator,
            actionInputJsElementObjectNamesFilter,
            actionParameterJsElementObjectNamesFilter,
            rootActionOnlyOnceAtStartParameterInitQueryJsGenerator,
            rootParametrizableActionInputEventsRegistrationJsGenerator,
            rootActionParametersCallbackJsGenerator,
            actionParametersCallbackFunctionNameGenerator);

        var rootSingleActionJsGenerator = new RootSingleActionJsGenerator(
            rootSingleParameterlessActionJsGenerator,
            rootSingleParametrizableActionJsGenerator);

        // Root generator
        var rootActionsJsGenerator = new RootActionsJsGenerator(rootSingleActionJsGenerator);

        return rootActionsJsGenerator;
    }
}
