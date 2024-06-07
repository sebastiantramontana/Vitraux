using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class PopulatingElementObjectName : ElementObjectName
{
    internal PopulatingElementObjectName(string name, string appendToName, PopulatingElementSelectorBase associatedSelector)
        : base(name, associatedSelector)
    {
        AppendToName = appendToName;
    }

    internal string AppendToName { get; }
}