using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IJsQueryFromTemplateElementsDeclaringGeneratorContext
    {
        IQueryElementsDeclaringJsCodeGenerator GetStrategy(PopulatingAppendToElementSelection selectionBy);
    }
}