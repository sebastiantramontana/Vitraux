using System.Text;
using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.CustomJsGeneration;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionJsCodeGenerator(
    ICollectionUpdateFunctionNameGenerator collectionUpdateFunctionNameGenerator,
    IUpdateTableCall updateTableCall,
    IUpdateCollectionByPopulatingElementsCall updateCollectionByPopulatingElementsCall,
    IUpdateCollectionFunctionCallbackJsCodeGenerator callbackJsCodeGenerator,
    ICustomJsJsGenerator customJsJsGenerator,
    ICodeFormatter codeFormatter,
    INotImplementedCaseGuard notImplementedCaseGuard)
    : IUpdateCollectionJsCodeGenerator
{
    public StringBuilder GenerateJs(StringBuilder jsBuilder, string parentObjectName, string collectionObjectName, JsCollectionNames jsCollectionNames, IUpdateViewJsGenerator updateViewJsGenerator, int currentIndentCount)
        => jsCollectionNames switch
        {
            JsCollectionElementObjectPairNames elementObjectPairNames => jsBuilder.Add(AddCollectionElementUpdateCall, parentObjectName, collectionObjectName, elementObjectPairNames, updateViewJsGenerator, currentIndentCount),
            JsCollectionCustomJsNames customJsNames => jsBuilder.Add(AddCollectionCustomJsCall, customJsNames.CustomJsTarget, parentObjectName, collectionObjectName, currentIndentCount),
            _ => notImplementedCaseGuard.ThrowException<StringBuilder>(jsCollectionNames)
        };

    private StringBuilder AddCollectionElementUpdateCall(StringBuilder jsBuilder, string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator, int currentIndentCount)
    {
        var functionName = collectionUpdateFunctionNameGenerator.Generate();
        var updateCall = GetUpdateCall(elementObjectPairNames, parentObjectName, collectionObjectName, functionName);

        return jsBuilder
            .AddTwoLines(callbackJsCodeGenerator.GenerateJs, elementObjectPairNames, updateViewJsGenerator, functionName, currentIndentCount)
            .Append(codeFormatter.IndentLine(updateCall, currentIndentCount));
    }

    private StringBuilder AddCollectionCustomJsCall(StringBuilder jsBuilder, CustomJsCollectionTarget customJsTarget, string parentObjectName, string collectionObjectName, int currentIndentCount)
    {
        var fullCollectionObjectName = CreateFullCollectionObjectName(parentObjectName, collectionObjectName);
        return jsBuilder.Add(customJsJsGenerator.Generate, customJsTarget, fullCollectionObjectName, currentIndentCount);
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