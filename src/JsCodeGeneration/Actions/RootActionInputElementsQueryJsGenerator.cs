using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionInputElementsQueryJsGenerator(IQueryElementsAlwaysJsCodeGenerator alwaysGenerator) : IRootActionInputElementsQueryJsGenerator
{
    private const string RootElementObjectName = "document";

    public StringBuilder GenerateJs(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsInputObjectNames)
        => jsBuilder.Append(alwaysGenerator.GenerateJsCode(jsInputObjectNames, RootElementObjectName));
}
