using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.CustomJsGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionJsCodeGenerator(
    IUpdateTableCall updateTableCall,
    IUpdateCollectionByPopulatingElementsCall updateCollectionByPopulatingElementsCall,
    IUpdateCollectionFunctionCallbackJsCodeGenerator callbackJsCodeGenerator,
    ICustomJsJsGenerator customJsJsGenerator,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IUpdateCollectionJsCodeGenerator
{
    public string GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionNames jsCollectionNames, IUpdateViewJsGenerator updateViewJsGenerator)
        => jsCollectionNames switch
        {
            JsCollectionElementObjectPairNames elementObjectPairNames => GetCollectionElementUpdateCall(parentObjectName, collectionObjectName, elementObjectPairNames, updateViewJsGenerator),
            JsCollectionCustomJsNames customJsNames => GetCollectionCustomJsCall(customJsNames.CustomJsTarget, parentObjectName, collectionObjectName),
            _ => notImplementedCaseGuard.ThrowException<string>(jsCollectionNames)
        };

    private string GetCollectionElementUpdateCall(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        var callbackInfo = callbackJsCodeGenerator.GenerateJs(elementObjectPairNames, updateViewJsGenerator);
        var updateCall = GetUpdateCall(elementObjectPairNames, parentObjectName, collectionObjectName, callbackInfo.FunctionName);

        return new StringBuilder()
                    .AppendLine(callbackInfo.JsCode)
                    .AppendLine()
                    .Append(updateCall)
                    .ToString();
    }

    private string GetCollectionCustomJsCall(CustomJsCollectionTarget customJsTarget, string parentObjectName, string collectionObjectName)
    {
        var fullCollectionObjectName = CreateFullCollectionObjectName(parentObjectName, collectionObjectName);
        return customJsJsGenerator.Generate(customJsTarget, fullCollectionObjectName);
    }

    private string GetUpdateCall(JsCollectionElementObjectPairNames elementObjectPairNames, string parentObjectName, string collectionObjectName, string updateFunctionCallbackName)
    {
        var fullCollectionObjectName = CreateFullCollectionObjectName(parentObjectName, collectionObjectName);
        var codeToCall = elementObjectPairNames.Target switch
        {
            CollectionTableTarget collectionTableTarget => updateTableCall.Generate(elementObjectPairNames.AppendToJsObjectName, collectionTableTarget.TBodyIndex, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName),
            CollectionElementTarget => updateCollectionByPopulatingElementsCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName),
            _ => notImplementedCaseGuard.ThrowException<string>(elementObjectPairNames.Target)
        };

        return $"{codeToCall};";
    }

    private static string CreateFullCollectionObjectName(string parentObjectName, string collectionObjectName)
        => $"{parentObjectName}.{collectionObjectName}";
}