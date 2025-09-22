using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal interface IStorageElementCollectionJsLineGenerator
{
    public string Generate(JsElementObjectName collectionObjectName);
}
