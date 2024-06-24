using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator<TViewModel>(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IElementNamesGenerator elementNamesGenerator,
    IValueNamesGenerator valueNamesGenerator,
    IValuesJsCodeGenerationBuilder valuesJsCodeGenerationBuilder)
    : IJsGenerator<TViewModel>
{
    public string GenerateJsCode(IModelMappingData modelMappingData, string parentObjectForValues, string elementNamePrefix)
    {
        IEnumerable<ElementSelectorBase> selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);
        IEnumerable<ElementObjectName> elements = elementNamesGenerator.Generate(elementNamePrefix, selectors);
        IEnumerable<ValueObjectName> valueNames = valueNamesGenerator.Generate(modelMappingData.Values);

        return new StringBuilder()
            .AppendLine(GenerateValuesJsCode(parentObjectForValues, valueNames, elements))
            .ToString();

    }

    private string GenerateValuesJsCode(string parentObjectForValues, IEnumerable<ValueObjectName> valueNames, IEnumerable<ElementObjectName> elements)
    {
        return valuesJsCodeGenerationBuilder
                .BuildJsCode(parentObjectForValues, valueNames, elements)
                .Trim();
    }
}
