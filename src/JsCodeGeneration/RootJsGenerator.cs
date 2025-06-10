using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;

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
        var initializeViewJs = GenerateInitializeViewJsCode(queryElementStrategy, objectNamesGrouping.AllJsElementObjectNames, ParentElementObjectName);
        var updateViewInfo = updateViewJsGenerator.GenerateJs(queryElementStrategy, objectNamesGrouping, ParentObjectName, ParentElementObjectName);

        return new(initializeViewJs, updateViewInfo);
    }

    private String GenerateInitializeViewJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsElementObjectNames, string parentElementObjectName)
        => initializeJsGeneratorContext.GetStrategy(strategy)
                .GenerateJs(allJsElementObjectNames, parentElementObjectName)
                .TrimEnd();
}
