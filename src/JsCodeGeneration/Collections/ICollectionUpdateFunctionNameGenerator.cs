namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionUpdateFunctionNameGenerator
{
    string Generate(string parentObjectName, string collectionObjectName, string appendToJsObjectName, string elementToInsertJsObjectName);
}



