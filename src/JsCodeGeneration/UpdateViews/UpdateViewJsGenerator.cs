using System.Text;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal class UpdateViewJsGenerator(
    IPromiseJsGenerator promiseJsGenerator,
    IValuesJsCodeGenerationBuilder valuesJsCodeGenerationBuilder,
    ICollectionsJsGenerationBuilder collectionsJsCodeGenerationBuilder,
    IQueryElementsJsCodeGeneratorContext queryElementsJsCodeGeneratorContext)
    : IUpdateViewJsGenerator
{
    public StringBuilder GenerateJs(
        StringBuilder jsBuilder,
        QueryElementStrategy queryElementStrategy,
        FullObjectNames fullObjectNames,
        string parentObjectName,
        string parentElementObjectName,
        int indentCount)
        => jsBuilder
            .TryAddTwoLines(GenerateQueryElementsJsCode, queryElementStrategy, fullObjectNames.JsElementObjectNames, parentElementObjectName, indentCount)
            .TryAddTwoLines(GenerateValuesJsCode, parentObjectName, fullObjectNames.ValueNames, indentCount)
            .TryAddTwoLines(GenerateCollectionJsCode, parentObjectName, fullObjectNames.CollectionNames, indentCount)
            .Add(promiseJsGenerator.GenerateJs, indentCount);

    private StringBuilder GenerateValuesJsCode(StringBuilder jsBuilder, string parentObjectName, IEnumerable<FullValueObjectName> valueNames, int indentCount)
        => jsBuilder.Add(valuesJsCodeGenerationBuilder.BuildJsCode, parentObjectName, valueNames, indentCount);

    private StringBuilder GenerateCollectionJsCode(StringBuilder jsBuilder, string parentObjectName, IEnumerable<FullCollectionObjectName> collectionNames, int indentCount)
        => jsBuilder.Add(collectionsJsCodeGenerationBuilder.BuildJsCode, parentObjectName, collectionNames, this, indentCount);

    private StringBuilder GenerateQueryElementsJsCode(StringBuilder jsBuilder, QueryElementStrategy strategy, IEnumerable<JsElementObjectName> allJsObjectNames, string parentElementObjectName, int indentCount)
        => queryElementsJsCodeGeneratorContext
            .GetStrategy(strategy)
            .GenerateJsCode(jsBuilder, allJsObjectNames, parentElementObjectName, indentCount);
}
