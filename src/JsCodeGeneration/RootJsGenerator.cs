using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration;

internal class RootJsGenerator(
    IInitializeJsGeneratorContext initializeJsGeneratorContext,
    IUpdateViewJsGenerator updateViewJsGenerator)
    : IRootJsGenerator
{
    private const string ParentObjectName = "vm";
    private const string ParentElementObjectName = "document";

    public GeneratedJsCode GenerateJs(FullObjectNames fullObjectNames, IEnumerable<JsObjectName> allJsElementObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var initializeViewJs = GenerateInitializeViewJsCode(queryElementStrategy, allJsElementObjectNames, ParentElementObjectName);
        var updateViewInfo = updateViewJsGenerator.GenerateJs(queryElementStrategy, fullObjectNames, allJsElementObjectNames, ParentObjectName, ParentElementObjectName);

        return new(initializeViewJs, updateViewInfo);
    }

    private string GenerateInitializeViewJsCode(QueryElementStrategy strategy, IEnumerable<JsObjectName> allJsElementObjectNames, string parentElementObjectName)
        => initializeJsGeneratorContext.GetStrategy(strategy)
                .GenerateJs(allJsElementObjectNames, parentElementObjectName)
                .TrimEnd();
}
