using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements
{
    internal interface IJsQueryFromTemplateElementsDeclaringGeneratorFactory
    {
        IQueryElementsDeclaringJsCodeGenerator GetInstance(PopulatingAppendToElementSelection selectionBy);
    }
}