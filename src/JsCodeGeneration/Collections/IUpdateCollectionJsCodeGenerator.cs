using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface IUpdateCollectionJsCodeGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, string parentObjectName, string collectionObjectName, JsCollectionNames jsCollectionNames, IUpdateViewJsGenerator updateViewJsGenerator, int currentIndentCount);
}