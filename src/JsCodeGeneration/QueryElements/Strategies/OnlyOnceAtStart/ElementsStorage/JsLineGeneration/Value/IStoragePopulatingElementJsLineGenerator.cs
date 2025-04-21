using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal interface IStoragePopulatingElementJsLineGenerator
{
    string Generate(string storedPopulatingElementCall, InsertElementObjectName populatingObjectName, string parentObjectToAppend);
}
