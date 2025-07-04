using System.Text;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration.UpdateViews;

internal class UpdateViewJsGenerator(
    IPromiseJsGenerator promiseJsGenerator,
    IValuesJsCodeGenerationBuilder valuesJsCodeGenerationBuilder,
    ICollectionsJsGenerationBuilder collectionsJsCodeGenerationBuilder,
    IQueryElementsJsCodeGeneratorContext queryElementsJsCodeGeneratorContext)
    : IUpdateViewJsGenerator
{
    public string GenerateJs(
        QueryElementStrategy queryElementStrategy,
        FullObjectNames fullObjectNames,
        string parentObjectName,
        string parentElementObjectName)
        => new StringBuilder()
                .Append(GenerateQueryElementsJsCode(queryElementStrategy, fullObjectNames.JsElementObjectNames, parentElementObjectName))
                .TryAppendLineForReadability()
                .Append(GenerateValuesJsCode(parentObjectName, fullObjectNames.ValueNames))
                .TryAppendLineForReadability()
                .Append(GenerateCollectionJsCode(parentObjectName, fullObjectNames.CollectionNames))
                .TryAppendLineForReadability()
                .Append(promiseJsGenerator.ReturnResolvedPromiseJsLine)
                .ToString()
                .TrimEnd();

    private string GenerateValuesJsCode(string parentObjectName, IEnumerable<FullValueObjectName> valueNames)
        => valuesJsCodeGenerationBuilder.BuildJsCode(parentObjectName, valueNames);

    private string GenerateCollectionJsCode(string parentObjectName, IEnumerable<FullCollectionObjectName> collectionNames)
        => collectionsJsCodeGenerationBuilder.BuildJsCode(parentObjectName, collectionNames, this);

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsObjectNames, string parentElementObjectName)
        => queryElementsJsCodeGeneratorContext
            .GetStrategy(strategy)
            .GenerateJsCode(allJsObjectNames, parentElementObjectName)
            .TrimEnd();
}
