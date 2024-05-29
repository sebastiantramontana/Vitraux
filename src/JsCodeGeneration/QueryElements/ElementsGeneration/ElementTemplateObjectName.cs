using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

internal record class ElementTemplateObjectName : ElementObjectName
{
    internal ElementTemplateObjectName(string name, string appendToName, ElementTemplateSelectorString associatedSelector)
        : base(name, associatedSelector)
    {
        AppendToName = appendToName;
    }

    internal string AppendToName { get; }
}