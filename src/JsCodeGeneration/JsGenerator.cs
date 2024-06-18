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
    IValuesJsCodeGenerationBuilder valuesJsCodeGenerationBuilder)
    : IJsGenerator<TViewModel>
{
    public string GenerateJsCode(IModelMappingData modelMappingData, ConfigurationBehavior configurationBehavior)
    {
        const string rootObject = "document";
        const string parentObjectForValues = "vm";

        return GenerateJsCode(modelMappingData, configurationBehavior.QueryElementStrategy, rootObject, parentObjectForValues, string.Empty);
    }

    private string GenerateJsCode(IModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string rootObject, string parentObjectForValues, string elementNamePrefix)
    {
        var selectors = GroupSelectors(modelMappingData);
        var elements = elementNamesGenerator.Generate(elementNamePrefix, selectors);
        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values);

        return new StringBuilder()
            .AppendLine(GenerateQueryElementsJsCode(queryElementStrategy, elements, rootObject))
            .AppendLine()
            .AppendLine(GenerateValuesJsCode(parentObjectForValues, valueNames, elements))
            .ToString()
            .Trim();
    }

    private string GenerateValuesJsCode(string parentObjectName, IEnumerable<ValueObjectName> valueNames, IEnumerable<ElementObjectName> elements)
        => valuesJsCodeGenerationBuilder
            .BuildJsCode(parentObjectName, valueNames, elements)
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
