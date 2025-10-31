using System.Text;
using Vitraux.JsCodeGeneration.Initialization;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.JsCodeGeneration;

internal class RootJsGenerator(
    IInitializeJsGeneratorContext initializeJsGeneratorContext,
    IUpdateViewJsGenerator updateViewJsGenerator)
    : IRootJsGenerator
{
    private const string ParentObjectName = "vm";
    private const string ParentElementObjectName = "document";
    private const int ZeroIndent = 0;

    public GeneratedJsCode GenerateJs(FullObjectNames fullObjectNames, QueryElementStrategy queryElementStrategy)
    {
        var initializeViewJs = GenerateInitializeViewJsCode(queryElementStrategy, fullObjectNames.JsElementObjectNames, ParentElementObjectName);
        var updateViewJsBuilder = updateViewJsGenerator.GenerateJs(new StringBuilder(), queryElementStrategy, fullObjectNames, ParentObjectName, ParentElementObjectName, ZeroIndent);

        return new(initializeViewJs, updateViewJsBuilder.ToString());
    }

    private string GenerateInitializeViewJsCode(QueryElementStrategy strategy, IEnumerable<JsElementObjectName> allJsElementObjectNames, string parentElementObjectName)
        => initializeJsGeneratorContext.GetStrategy(strategy)
                .GenerateJs(allJsElementObjectNames, parentElementObjectName)
                .TrimEnd();
}
