namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionsJsGenerationBuilder
{
    string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collectionObjectNames, IJsGenerator jsGenerator);
}



