using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionJsCodeGenerator(
    IUpdateTableCall updateTableCall,
    IUpdateCollectionByPopulatingElementsCall updateCollectionByPopulatingElementsCall,
    IUpdateCollectionFunctionCallbackJsCodeGenerator callbackJsCodeGenerator)
    : IUpdateCollectionJsCodeGenerator
{
    public GeneratedUpdateCollectionJs GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator)
    {
        var callbackInfo = callbackJsCodeGenerator.GenerateJs(parentObjectName, collectionObjectName, elementObjectPairNames, updateViewJsGenerator);
        var updateCall = GetUpdateCall(elementObjectPairNames, parentObjectName, collectionObjectName, callbackInfo.FunctionName);

        var updateCollectionjsCode = new StringBuilder()
                                        .AppendLine(callbackInfo.JsCode)
                                        .AppendLine()
                                        .Append(updateCall)
                                        .ToString();

        return new(updateCollectionjsCode, callbackInfo.ViewModelSerializationData);
    }

    private string GetUpdateCall(JsCollectionElementObjectPairNames elementObjectPairNames, string parentObjectName, string collectionObjectName, string updateFunctionCallbackName)
    {
        var fullCollectionObjectName = CreateFullCollectionObjectName(parentObjectName, collectionObjectName);
        var codeToCall = elementObjectPairNames.Target is CollectionTableTarget
                        ? updateTableCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName)
                        : updateCollectionByPopulatingElementsCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName);

        return $"{codeToCall};";
    }

    private static string CreateFullCollectionObjectName(string parentObjectName, string collectionObjectName)
        => $"{parentObjectName}.{collectionObjectName}";
}



