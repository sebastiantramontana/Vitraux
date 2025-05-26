using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class UpdateCollectionJsCodeGenerator(
    IUpdateTableCall updateTableCall,
    IUpdateCollectionByPopulatingElementsCall updateCollectionByPopulatingElementsCall,
    IUpdateCollectionFunctionCallbackJsCodeGenerator callbackJsCodeGenerator)
    : IUpdateCollectionJsCodeGenerator
{
    public string GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator)
    {
        var callbackInfo = callbackJsCodeGenerator.GenerateJsCode(parentObjectName, collectionObjectName, elementObjectPairNames, jsGenerator);
        var updateCall = GetUpdateCall(elementObjectPairNames, parentObjectName, collectionObjectName, callbackInfo.FunctionName);

        return new StringBuilder()
            .AppendLine(callbackInfo.JsCode)
            .AppendLine()
            .Append(updateCall)
            .ToString();
    }

    private string GetUpdateCall(JsCollectionElementObjectPairNames elementObjectPairNames, string parentObjectName, string collectionObjectName, string updateFunctionCallbackName)
    {
        var fullCollectionObjectName = $"{parentObjectName}.{collectionObjectName}";
        var codeToCall = elementObjectPairNames.Target is CollectionTableTarget
                        ? updateTableCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName)
                        : updateCollectionByPopulatingElementsCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName);

        return $"{codeToCall};";
    }
}



