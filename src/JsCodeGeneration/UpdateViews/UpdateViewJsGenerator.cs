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
    public UpdateViewInfo GenerateJs(
        QueryElementStrategy queryElementStrategy,
        JsObjectNamesGrouping objectNamesGrouping,
        string parentObjectName,
        string parentElementObjectName)
    {
        var builtValueJs = GenerateValuesJsCode(parentObjectName, objectNamesGrouping.ValueNames);
        var builtCollectionJs = GenerateCollectionJsCode(parentObjectName, objectNamesGrouping.CollectionNames);

        var jsCode= new StringBuilder()
                .Append(GenerateQueryElementsJsCode(queryElementStrategy, objectNamesGrouping.AllJsElementObjectNames, parentElementObjectName))
                .TryAppendLineForReadability()
                .Append(builtValueJs.JsCode)
                .TryAppendLineForReadability()
                .Append(builtCollectionJs.JsCode)
                .TryAppendLineForReadability()
                .Append(promiseJsGenerator.ReturnResolvedPromiseJsLine)
                .ToString()
                .TrimEnd();

        var viewModelSerializationData = new ViewModelSerializationData(builtValueJs.ValueViewModelSerializationsData, builtCollectionJs.CollectionViewModelSerializationsData);
        return new UpdateViewInfo(jsCode, viewModelSerializationData);
    }

    private BuiltValueJs GenerateValuesJsCode(string parentObjectName, IEnumerable<FullValueObjectName> valueNames)
        => valuesJsCodeGenerationBuilder.BuildJsCode(parentObjectName, valueNames);

    private BuiltCollectionJs GenerateCollectionJsCode(string parentObjectName, IEnumerable<FullCollectionObjectName> collectionNames)
        => collectionsJsCodeGenerationBuilder.BuildJsCode(parentObjectName, collectionNames, this);

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsObjectNames, string parentElementObjectName)
        => queryElementsJsCodeGeneratorContext
                    .GetStrategy(strategy)
                    .GenerateJsCode(allJsObjectNames, parentElementObjectName)
                    .TrimEnd();
}
