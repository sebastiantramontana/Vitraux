﻿using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    UpdateCollectionFunctionCallbackInfo GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator);
}