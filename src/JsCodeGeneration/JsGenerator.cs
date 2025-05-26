using System.Text;
using System.Xml.Linq;
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
            .AppendLine(GenerateQueryElementsJsCode(queryElementStrategy, allJsObjectNames, parentElementObjectName))
            .AppendLine()
            .AppendLine(GenerateValuesJsCode(parentObjectName, valueNames, allJsObjectNames))
            .AppendLine()
            .AppendLine(GenerateCollectionJsCode(parentObjectName, collectionNames))
            .AppendLine()
            .AppendLine(ReturnedResolvedPromise)
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

internal interface ICollectionsJsGenerationBuilder
{
    string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collectionObjectNames, IJsGenerator jsGenerator);
}

internal class CollectionsJsGenerationBuilder(
    IPropertyCheckerJsCodeGeneration propertyChecker,
    IUpdateCollectionJsCodeGenerator updateCollectionJsCodeGenerator)
    : ICollectionsJsGenerationBuilder
{
    public string BuildJsCode(string parentObjectName, IEnumerable<CollectionObjectName> collections, IJsGenerator jsGenerator)
        => collections.Aggregate(new StringBuilder(), (sb, collection) =>
        {
            return collection
                    .AssociatedElementNames
                    .Aggregate(sb, (sb2, associatedElement) =>
                    {
                        var updateCollectionjsCode = updateCollectionJsCodeGenerator.GenerateJs(parentObjectName, collection.Name, associatedElement, jsGenerator);
                        var propertyCheckerJsCode = propertyChecker.GenerateJs(parentObjectName, collection.Name, updateCollectionjsCode);
                        return sb2.AppendLine(propertyCheckerJsCode);
                    });
        })
        .ToString()
        .TrimEnd();
}

internal interface IUpdateCollectionJsCodeGenerator
{
    string GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator);
}

internal class UpdateCollectionJsCodeGenerator(
    IUpdateTableCall updateTableCall,
    IUpdateCollectionByPopulatingElementsCall updateCollectionByPopulatingElementsCall,
    IUpdateCollectionFunctionCallbackJsCodeGenerator callbackJsCodeGenerator)
    : IUpdateCollectionJsCodeGenerator
{
    public string GenerateJs(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator)
    {
        var callbackInfo = callbackJsCodeGenerator.GenerateJsCode(parentObjectName, collectionObjectName, elementObjectPairNames, jsGenerator);
        var updateCall = GetUpdateCall(elementObjectPairNames, parentObjectName, collectionObjectName, callbackInfo.FunctionName);

        return new StringBuilder()
            .AppendLine(callbackInfo.JsCode)
            .AppendLine()
            .Append(updateCall)
            .ToString();
    }

    private string GetUpdateCall(JsCollectionElementObjectPairNames elementObjectPairNames, string parentObjectName, string collectionObjectName, string updateFunctionCallbackName)
    {
        var fullCollectionObjectName = $"{parentObjectName}.{collectionObjectName}";
        var codeToCall = elementObjectPairNames.Target is CollectionTableTarget
                        ? updateTableCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName)
                        : updateCollectionByPopulatingElementsCall.Generate(elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName, updateFunctionCallbackName, fullCollectionObjectName);

        return $"{codeToCall};";
    }
}

internal interface IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    UpdateCollectionFunctionCallbackInfo GenerateJsCode(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator);
}

internal class UpdateCollectionFunctionCallbackJsCodeGenerator(
    ICollectionUpdateFunctionNameGenerator collectionUpdateFunctionNameGenerator,
    ICodeFormatter codeFormatter) : IUpdateCollectionFunctionCallbackJsCodeGenerator
{
    public UpdateCollectionFunctionCallbackInfo GenerateJsCode(string parentObjectName, string collectionObjectName, JsCollectionElementObjectPairNames elementObjectPairNames, IJsGenerator jsGenerator)
    {
        const string CollectionItemObjectName = "collectionItem";
        const string ParentElementObjectName = "parent";

        var functionName = collectionUpdateFunctionNameGenerator.Generate(parentObjectName, collectionObjectName, elementObjectPairNames.AppendToJsObjectName, elementObjectPairNames.ElementToInsertJsObjectName);
        var elementNamePrefix = $"{parentObjectName.Replace('.', '_')}_{collectionObjectName}";
        var generatedJs = jsGenerator.GenerateJsCode(elementObjectPairNames.Target.Data, QueryElementStrategy.Always, CollectionItemObjectName, ParentElementObjectName, elementNamePrefix);

        var jsCode = new StringBuilder()
            .AppendLine($"const {functionName} = async ({ParentElementObjectName}, {CollectionItemObjectName}) =>")
            .AppendLine("{")
            .AppendLine(codeFormatter.Indent(generatedJs))
            .Append('}')
            .ToString();

        return new UpdateCollectionFunctionCallbackInfo(functionName, jsCode);
    }
}

internal interface ICollectionUpdateFunctionNameGenerator
{
    string Generate(string parentObjectName, string collectionObjectName, string appendToJsObjectName, string elementToInsertJsObjectName);
}

internal class CollectionUpdateFunctionNameGenerator : ICollectionUpdateFunctionNameGenerator
{
    public string Generate(string parentObjectName, string collectionObjectName, string appendToJsObjectName, string elementToInsertJsObjectName)
        => $"updateCollection_{parentObjectName.Replace('.', '_')}_{collectionObjectName}_{appendToJsObjectName}_{elementToInsertJsObjectName}";
}

internal record class UpdateCollectionFunctionCallbackInfo(string FunctionName, string JsCode);



