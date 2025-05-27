namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionUpdateFunctionNameGenerator : ICollectionUpdateFunctionNameGenerator
{
    const string FunctionNamePrefix = "uc";

    public string Generate(string parentObjectName, string collectionObjectName, string appendToJsObjectName, string elementToInsertJsObjectName)
        => $"{FunctionNamePrefix}_{parentObjectName.Replace('.', '_')}_{collectionObjectName}_{appendToJsObjectName}_{elementToInsertJsObjectName}";
}



