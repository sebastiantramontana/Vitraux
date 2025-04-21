using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Selectors.Values;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IJsObjectNamesGenerator jsObjectNamesGenerator,
    IValueNamesGenerator valueNamesGenerator,
    ICollectionNamesGenerator collectionNamesGenerator,
    IValuesJsCodeGenerationBuilder valuesJsCodeGenerationBuilder,
    ICollectionsJsCodeGenerationBuilder collectionsJsCodeGenerationBuilder,
    IQueryElementsJsCodeGeneratorByStrategyContext queryElementsJsCodeGeneratorContext)
    : IJsGenerator
{
    public string GenerateJsCode(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix)
    {
        var selectors = uniqueSelectorsFilter.FilterDistinct(modelMappingData);
        var allJsObjectNames = jsObjectNamesGenerator.Generate(elementNamePrefix, selectors);

        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values);
        var collectionNames = collectionNamesGenerator.Generate(modelMappingData.Collections);

        return new StringBuilder()
            .AppendLine(GenerateQueryElementsJsCode(queryElementStrategy, allJsObjectNames, parentElementObjectName))
            .AppendLine()
            .AppendLine(GenerateValuesJsCode(parentObjectName, valueNames, allJsObjectNames))
            .AppendLine()
            .AppendLine(GenerateCollectionJsCode(parentObjectName, collectionNames, allJsObjectNames))
            .ToString()
            .TrimEnd();
    }


    private string GenerateValuesJsCode(string parentObjectName, IEnumerable<ValueObjectName> valueNames, IEnumerable<JsObjectName> allJsObjectNames)
        => valuesJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, valueNames, allJsObjectNames)
                .TrimEnd();

    private string GenerateCollectionJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collectionNames, IEnumerable<JsObjectName> allJsObjectNames)
        => collectionsJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, collectionNames, allJsObjectNames, this)
                .TrimEnd();

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsObjectNames, string parentElementObjectName)
        => queryElementsJsCodeGeneratorContext
                    .GetStrategy(strategy) //SEGUIR CON CADA UNA DE LAS STRATEGIES: collectionElementObjectNames!!!
                    .GenerateJsCode(allElementObjectNames, collectionElementObjectNames, parentElementObjectName)
                    .TrimEnd();
}

internal interface ICollectionsJsCodeGenerationBuilder
{
    string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collectionObjectNames, IEnumerable<ElementObjectName> elements, IJsGenerator jsGenerator);
}

internal class CollectionsJsCodeGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsCodeGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collections, IEnumerable<ElementObjectName> elements, IJsGenerator jsGenerator)
        => collections
                .Aggregate(new StringBuilder(), (sb, collection) =>
                {
                    var associatedElements = GetElementNamesAssociatedToCollection(elements, collection.AssociatedCollection.CollectionSelector);
                    return associatedElements.Aggregate(sb, (sb, associatedElement) => sb.AppendLine(propertyChecker.GenerateJsCode(parentObjectName, collection.Name, updateCollectionJsCodeGenerator.GenerateJsCode(collection, associatedElement, jsGenerator))));
                })
                .ToString()
                .TrimEnd();

    private static IEnumerable<ElementObjectName> GetElementNamesAssociatedToCollection(IEnumerable<ElementObjectName> elements, ElementSelectorBase appendToSelector)
        => elements.Where(e => e.AssociatedSelector == appendToSelector);
}

internal interface IUpdateCollectionJsCodeGenerator
{
    string GenerateJsCode(CollectionObjectName collectionObjectName, ElementObjectName element, IJsGenerator jsGenerator);
}

internal class UpdateCollectionJsCodeGenerator(
    IUpdateTableCall updateTableCall,
    IUpdateCollectionByPopulatingElementsCall updateCollectionByPopulatingElementsCall,
    IUpdateCollectionFunctionCallbackJsCodeGenerator callbackJsCodeGenerator)
    : IUpdateCollectionJsCodeGenerator
{
    public string GenerateJsCode(CollectionObjectName collectionObjectName, ElementObjectName element, IJsGenerator jsGenerator)
    {
        var callbackInfo = callbackJsCodeGenerator.GenerateJsCode(collectionObjectName.AssociatedCollection.ModelMappingData, jsGenerator);
        var updateCall = GetUpdateCall(element as InsertElementObjectName, collectionObjectName, callbackInfo.FunctionName);

        return new StringBuilder()
            .AppendLine(callbackInfo.JsCode)
            .AppendLine()
            .Append(updateCall)
            .ToString();
    }

    private string GetUpdateCall(InsertElementObjectName populatingElementObjectName, CollectionObjectName collectionObjectName, string updateFunctionCallbackName)
    {
        return collectionObjectName.AssociatedCollection is CollectionTableTarget
                ? $"{updateTableCall.Generate(populatingElementObjectName.AppendToJsObjNameName, populatingElementObjectName.JsObjName, updateFunctionCallbackName, collectionObjectName.Name)};"
                : $"{updateCollectionByPopulatingElementsCall.Generate(populatingElementObjectName.AppendToJsObjNameName, populatingElementObjectName.JsObjName, updateFunctionCallbackName, collectionObjectName.Name)};";
    }
}

internal interface IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    UpdateCollectionFunctionCallbackInfo GenerateJsCode(ModelMappingData modelMappingData, IJsGenerator jsGenerator);
}

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(IRandomStringGenerator randomStringGenerator, ICodeFormatter codeFormatter) : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    public UpdateCollectionFunctionCallbackInfo GenerateJsCode(ModelMappingData modelMappingData, IJsGenerator jsGenerator)
    {
        const string CollectionItemObjectName = "collectionItem";
        const string ParentElementObjectName = "parent";

        var randomFunctionNamePostfix = randomStringGenerator.Generate();
        var functionName = $"updateCollection{randomFunctionNamePostfix}";

        var jsCode = new StringBuilder()
            .AppendLine($"const {functionName} = ({ParentElementObjectName}, {CollectionItemObjectName}) =>")
            .AppendLine("{")
            .AppendLine(codeFormatter.Indent(jsGenerator.GenerateJsCode(modelMappingData, QueryElementStrategy.Always, CollectionItemObjectName, ParentElementObjectName, $"_{randomFunctionNamePostfix}_")))
            .Append('}')
            .ToString();

        return new UpdateCollectionFunctionCallbackInfo(functionName, jsCode);
    }
}

internal interface IRandomStringGenerator
{
    string Generate();
}

internal class RandomStringGenerator : IRandomStringGenerator
{
    public string Generate() => Guid.NewGuid().ToString("N");
}

internal record class UpdateCollectionFunctionCallbackInfo(string FunctionName, string JsCode);



