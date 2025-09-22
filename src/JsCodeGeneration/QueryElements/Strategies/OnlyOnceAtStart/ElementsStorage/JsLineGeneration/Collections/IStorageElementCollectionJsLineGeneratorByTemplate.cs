using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal interface IStorageElementCollectionJsLineGeneratorByTemplate
{
    string Generate(JsElementObjectName collectionObjectName);
}
