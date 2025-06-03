using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator(
    IJsObjectNamesGenerator jsObjectNamesGenerator,
    IUpdateViewJsGenerator updateViewJsGenerator,
    IInitializeJsGeneratorContext initializeJsGeneratorContext)
    : IJsGenerator
{
    public GeneratedJsCode GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy, string parentObjectName, string parentElementObjectName, string elementNamePrefix)
    {
        var objectNamesGrouping = jsObjectNamesGenerator.Generate(modelMappingData, elementNamePrefix);
        var initializeViewJs = GenerateInitilizeViewJsCode(queryElementStrategy, objectNamesGrouping.AllJsElementObjectNames, parentElementObjectName);
        var updateViewJs = updateViewJsGenerator.GenerateJs(queryElementStrategy, objectNamesGrouping, parentObjectName, parentElementObjectName);
        var valueObjects = objectNamesGrouping.ValueNames.Select(v => new ValueObjectNameWithData(v.Name, v.AssociatedData));
        var collectionObjects = objectNamesGrouping.CollectionNames.Select(c => new CollectionObjectNameWithData(c.Name, c.AssociatedData));

        return new(initializeViewJs, updateViewJs, valueObjects, collectionObjects);
    }

    private String GenerateInitilizeViewJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsElementObjectNames, string parentElementObjectName)
        => initializeJsGeneratorContext.GetStrategy(strategy)
                .GenerateJs(allJsElementObjectNames, parentElementObjectName)
                .TrimEnd();
}