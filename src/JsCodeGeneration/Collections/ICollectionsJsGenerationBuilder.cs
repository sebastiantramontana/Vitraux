using System.Text;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionsJsGenerationBuilder
{
    StringBuilder BuildJsCode(StringBuilder jsBuilder, string parentObjectName, IEnumerable<FullCollectionObjectName> collectionObjectNames, IUpdateViewJsGenerator updateViewJsGenerator, int indentCount);
}