using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration;

internal interface IStoragePopulatingElementJsLineGenerator
{
    string Generate(string storedPopulatingElementCall, PopulatingElementObjectName populatingObjectName, string parentObjectToAppend);
}
