namespace Vitraux.JsCodeGeneration.Collections;

internal interface IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    UpdateCollectionFunctionCallbackInfo GenerateJsCode(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator);
}



