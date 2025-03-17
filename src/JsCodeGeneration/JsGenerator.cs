using System.Text;
using Vitraux.JsCodeGeneration.BuiltInCalling.Updating;
using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator(
    IUniqueSelectorsFilter uniqueSelectorsFilter,
    IElementNamesGenerator elementNamesGenerator,
    ICollectionElementNamesGenerator collectionElementNamesGenerator,
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
        var allElementObjectNames = elementNamesGenerator.Generate(elementNamePrefix, selectors.ElementSelectors);
        var collectionElementObjectNames = collectionElementNamesGenerator.GenerateInsertionNames(selectors.CollectionSelectors, allElementObjectNames);

        var valueNames = valueNamesGenerator.Generate(modelMappingData.Values);
        var collectionNames = collectionNamesGenerator.Generate(modelMappingData.CollectionElements);

        return new StringBuilder()
            .AppendLine(GenerateQueryElementsJsCode(queryElementStrategy, allElementObjectNames, collectionElementObjectNames, parentElementObjectName))
            .AppendLine()
            .AppendLine(GenerateValuesJsCode(parentObjectName, valueNames, allElementObjectNames))
            .AppendLine()
            .AppendLine(GenerateCollectionJsCode(parentObjectName, collectionNames, allElementObjectNames))
            .ToString()
            .TrimEnd();
    }

    private string GenerateValuesJsCode(string parentObjectName, IEnumerable<ValueObjectName> valueNames, IEnumerable<ElementObjectName> elements)
        => valuesJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, valueNames, elements)
                .TrimEnd();

    private string GenerateCollectionJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collectionNames, IEnumerable<ElementObjectName> elements)
        => collectionsJsCodeGenerationBuilder
                .BuildJsCode(parentObjectName, collectionNames, elements, this)
                .TrimEnd();

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<ElementObjectName> allElementObjectNames, IEnumerable<CollectionElementObjectName> collectionElementObjectNames, string parentElementObjectName)
        => queryElementsJsCodeGeneratorContext
                    .GetStrategy(strategy) SEGUIR CON CADA UNA DE LAS STRATEGIES: collectionElementObjectNames!!!
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
        var updateCall = GetUpdateCall(element as PopulatingElementObjectName, collectionObjectName, callbackInfo.FunctionName);

        return new StringBuilder()
            .AppendLine(callbackInfo.JsCode)
            .AppendLine()
            .Append(updateCall)
            .ToString();
    }

    private string GetUpdateCall(PopulatingElementObjectName populatingElementObjectName, CollectionObjectName collectionObjectName, string updateFunctionCallbackName)
    {
        return collectionObjectName.AssociatedCollection is CollectionTableModel
                ? $"{updateTableCall.Generate(populatingElementObjectName.AppendToName, populatingElementObjectName.Name, updateFunctionCallbackName, collectionObjectName.Name)};"
                : $"{updateCollectionByPopulatingElementsCall.Generate(populatingElementObjectName.AppendToName, populatingElementObjectName.Name, updateFunctionCallbackName, collectionObjectName.Name)};";
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



