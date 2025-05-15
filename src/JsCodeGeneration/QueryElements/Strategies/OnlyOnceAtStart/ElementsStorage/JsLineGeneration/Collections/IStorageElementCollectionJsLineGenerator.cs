using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Collections;

internal interface IStorageElementCollectionJsLineGenerator
{
    public string Generate(JsObjectName collectionObjectName);
}
