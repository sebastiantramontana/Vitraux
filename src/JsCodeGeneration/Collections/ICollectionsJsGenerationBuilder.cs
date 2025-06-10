using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionsJsGenerationBuilder
{
    BuiltCollectionJs BuildJsCode(string parentObjectName, IEnumerable<FullCollectionObjectName> collectionObjectNames, IUpdateViewJsGenerator updateViewJsGenerator);
}