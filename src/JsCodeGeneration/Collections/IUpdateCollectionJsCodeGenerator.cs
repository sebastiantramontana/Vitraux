using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface IUpdateCollectionJsCodeGenerator
{
    string GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator);
}



