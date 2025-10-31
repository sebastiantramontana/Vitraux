using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, JsCollectionElementObjectPairNames elementObjectPairNames, IUpdateViewJsGenerator updateViewJsGenerator, string functionName, int currentIndentCount);
}