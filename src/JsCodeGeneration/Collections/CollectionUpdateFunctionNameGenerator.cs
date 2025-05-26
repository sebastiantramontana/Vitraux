namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionUpdateFunctionNameGenerator : ICollectionUpdateFunctionNameGenerator
{
    public string Generate(string parentObjectName, string collectionObjectName, string appendToJsObjectName, string elementToInsertJsObjectName)
        => $"updateCollection_{parentObjectName.Replace('.', '_')}_{collectionObjectName}_{appendToJsObjectName}_{elementToInsertJsObjectName}";
}



