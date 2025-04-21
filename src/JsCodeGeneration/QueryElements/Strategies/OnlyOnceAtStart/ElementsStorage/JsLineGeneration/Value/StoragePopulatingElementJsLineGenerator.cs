using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StoragePopulatingElementJsLineGenerator(IStoragePopulatingAppendToElementJsLineGenerator storagePopulatingAppendToJsLineGenerator)
    : IStoragePopulatingElementJsLineGenerator
{
    public string Generate(string storedPopulatingElementCall, InsertElementObjectName populatingObjectName, string parentObjectToAppend)
    {
        var associatedPopulatingSelector = populatingObjectName!.AssociatedSelector as InsertElementSelectorBase;

        return new StringBuilder()
            .AppendLine(storedPopulatingElementCall)
            .Append(storagePopulatingAppendToJsLineGenerator.Generate(associatedPopulatingSelector!.ElementToAppend, populatingObjectName.AppendToJsObjNameName, parentObjectToAppend))
            .ToString();
    }
}
