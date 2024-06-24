using System.Text;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal class RootJsGenerator<TViewModel>(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IElementNamesGenerator elementNamesGenerator,
    IQueryElementsJsCodeGeneratorByStrategyContext queryElementsJsCodeGeneratorContext,
    IJsGenerator<TViewModel> jsGenerator)
    : IRootJsGenerator<TViewModel>
{
    public string GenerateJsCode(IModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy)
    {
        const string rootObject = "document";
        const string parentObjectForValues = "vm";

        IEnumerable<ElementSelectorBase> selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);
        IEnumerable<ElementObjectName> elements = elementNamesGenerator.Generate(selectors);

        return new StringBuilder()
            .AppendLine(GenerateQueryElementsJsCode(queryElementStrategy, elements, rootObject))
            .AppendLine()
            .AppendLine(jsGenerator.GenerateJsCode(modelMappingData, parentObjectForValues, string.Empty))
            .ToString()
            .Trim();
    }

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<ElementObjectName> elements, string rootObject)
    {
        return queryElementsJsCodeGeneratorContext
                    .GetStrategy(strategy)
                    .GenerateJsCode(elements, rootObject)
                    .Trim();
    }
}
