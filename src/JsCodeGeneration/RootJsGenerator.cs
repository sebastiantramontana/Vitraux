using Vitraux.JsCodeGeneration.Collections;
using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;
using Vitraux.JsCodeGeneration.Values;

namespace Vitraux.JsCodeGeneration;

internal class RootJsGenerator(
    IJsObjectNamesGenerator jsObjectNamesGenerator,
    IInitializeJsGeneratorContext initializeJsGeneratorContext,
    IUpdateViewJsGenerator updateViewJsGenerator)
    : IRootJsGenerator
{
    private const string ParentObjectName = "vm";
    private const string ParentElementObjectName = "document";

    public GeneratedJsCode GenerateJs(ModelMappingData modelMappingData, QueryElementStrategy queryElementStrategy)
    {
        var objectNamesGrouping = jsObjectNamesGenerator.Generate(modelMappingData, string.Empty);
        var initializeViewJs = GenerateInitilizeViewJsCode(queryElementStrategy, objectNamesGrouping.AllJsElementObjectNames, ParentElementObjectName);
        var updateViewJs = updateViewJsGenerator.GenerateJs(queryElementStrategy, objectNamesGrouping, ParentObjectName, ParentElementObjectName);
        var valueObjects = objectNamesGrouping.ValueNames.Select(v => new ValueObjectNameWithData(v.Name, v.AssociatedData));
        var collectionObjects = objectNamesGrouping.CollectionNames.Select(c => new CollectionObjectNameWithData(c.Name, c.AssociatedData));

        return new(initializeViewJs, updateViewJs, valueObjects, collectionObjects);

    }

    private String GenerateInitilizeViewJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsElementObjectNames, string parentElementObjectName)
        => initializeJsGeneratorContext.GetStrategy(strategy)
                .GenerateJs(allJsElementObjectNames, parentElementObjectName)
                .TrimEnd();
}
