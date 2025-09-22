using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal interface IStorageElementCollectionJsLineGeneratorByUri
{
    string Generate(JsElementObjectName collectionObjectName);
}
