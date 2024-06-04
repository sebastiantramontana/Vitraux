using System.Text;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator<TViewModel>(
    IQueryElementsJsCodeGeneratorByStrategyContext queryElementsJsCodeGeneratorContext,
    IElementNamesGenerator elementNamesGenerator,
    IValueNamesGenerator valueNamesGenerator,
    IValuesJsCodeGenerator valueJsCodeGenerator)
    : IJsGenerator<TViewModel>
{
    public string GenerateJsCode(IModelMappingData modelMappingData, ConfigurationBehavior configurationBehavior)
    {
        var rootObject = "document";
        var selectors = GroupSelectors(modelMappingData);
        var elements = elementNamesGenerator.Generate(selectors);
        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values);

        return new StringBuilder()
            .AppendLine(GenerateQueryElementsJsCode(configurationBehavior.QueryElementStrategy, elements, rootObject))
            .AppendLine()
            .AppendLine(GenerateValuesJsCode(valueNames, elements))
            .ToString()
            .Trim();
    }

    private string GenerateValuesJsCode(IEnumerable<ValueObjectName> valueNames, IEnumerable<ElementObjectName> elements)
        => valueJsCodeGenerator
            .GenerateJsCode(valueNames, elements)
            .Trim();

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<ElementObjectName> elements, string rootObject)
        => queryElementsJsCodeGeneratorContext
                .GetStrategy(strategy)
                .GenerateJsCode(elements, rootObject)
                .Trim();

    private static IEnumerable<ElementSelectorBase> GroupSelectors(IModelMappingData modelMappingData)
        => modelMappingData
            .Values
            .SelectMany(v => v.TargetElements.Select(te => te.Selector))
            .Distinct()
            .Concat(modelMappingData.CollectionElements.Select(c => c.ElementSelector));
}
