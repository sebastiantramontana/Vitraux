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
        JsObjectNamesGrouping objectNamesGrouping,
        string parentObjectName,
        string parentElementObjectName)
        => new StringBuilder()
            .Append(GenerateQueryElementsJsCode(queryElementStrategy, objectNamesGrouping.AllJsElementObjectNames, parentElementObjectName))
            .TryAppendLineForReadability()
            .Append(GenerateValuesJsCode(parentObjectName, objectNamesGrouping.ValueNames))
            .TryAppendLineForReadability()
            .Append(GenerateCollectionJsCode(parentObjectName, objectNamesGrouping.CollectionNames))
            .TryAppendLineForReadability()
            .Append(promiseJsGenerator.ReturnResolvedPromiseJsLine)
            .ToString()
            .TrimEnd();

    private string GenerateValuesJsCode(string parentObjectName, IEnumerable<ValueObjectNameWithJsTargets> valueNames)
        => valuesJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, valueNames)
                .TrimEnd();

    private string GenerateCollectionJsCode(string parentObjectName, IEnumerable<CollectionObjectNameWithElements> collectionNames)
        => collectionsJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, collectionNames, this)
                .TrimEnd();

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsObjectNames, string parentElementObjectName)
        => queryElementsJsCodeGeneratorContext
                    .GetStrategy(strategy)
                    .GenerateJsCode(allJsObjectNames, parentElementObjectName)
                    .TrimEnd();
}
