using System.Text;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IJsObjectNamesGenerator jsObjectNamesGenerator,
    IValueNamesGenerator valueNamesGenerator,
    ICollectionNamesGenerator collectionNamesGenerator,
    IValuesJsCodeGenerationBuilder valuesJsCodeGenerationBuilder,
    ICollectionsJsGenerationBuilder collectionsJsCodeGenerationBuilder,
    IQueryElementsJsCodeGeneratorContext queryElementsJsCodeGeneratorContext)
    : IJsGenerator
{
    public string GenerateJsCode(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix)
    {
        const string ReturnedResolvedPromise = "return Promise.resolve();";

        var selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);
        var allJsObjectNames = jsObjectNamesGenerator.Generate(elementNamePrefix, selectors);

        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values);
        var collectionNames = collectionNamesGenerator.Generate(modelMappingData.Collections, allJsObjectNames);

        return new StringBuilder()
            .Append(GenerateQueryElementsJsCode(queryElementStrategy, allJsObjectNames, parentElementObjectName))
            .TryAppendLineForReadability()
            .Append(GenerateValuesJsCode(parentObjectName, valueNames, allJsObjectNames))
            .TryAppendLineForReadability()
            .Append(GenerateCollectionJsCode(parentObjectName, collectionNames))
            .TryAppendLineForReadability()
            .Append(ReturnedResolvedPromise)
            .ToString()
            .TrimEnd();
    }


    private string GenerateValuesJsCode(string parentObjectName, IEnumerable<ValueObjectName> valueNames, IEnumerable<JsObjectName> allJsObjectNames)
        => valuesJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, valueNames, allJsObjectNames)
                .TrimEnd();

    private string GenerateCollectionJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collectionNames)
        => collectionsJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, collectionNames, this)
                .TrimEnd();

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsObjectNames, string parentElementObjectName)
        => queryElementsJsCodeGeneratorContext
                    .GetStrategy(strategy)
                    .GenerateJsCode(allJsObjectNames, parentElementObjectName)
                    .TrimEnd();
}