using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StoragePopulatingElementJsLineGenerator(IStoragePopulatingAppendToElementJsLineGenerator storagePopulatingAppendToJsLineGenerator)
    : IStoragePopulatingElementJsLineGenerator
{
    public string Generate(string storedPopulatingElementCall, PopulatingElementObjectName populatingObjectName, string parentObjectToAppend)
    {
        var associatedPopulatingSelector = populatingObjectName!.AssociatedSelector as PopulatingElementSelectorBase;

        return new StringBuilder()
            .AppendLine(storedPopulatingElementCall)
            .Append(storagePopulatingAppendToJsLineGenerator.Generate(associatedPopulatingSelector!.ElementToAppend, populatingObjectName.AppendToName, parentObjectToAppend))
            .ToString();
    }
}
