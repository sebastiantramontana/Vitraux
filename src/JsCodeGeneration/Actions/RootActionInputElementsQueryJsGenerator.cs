using System.Text;
using Vitraux.JsCodeGeneration.Formating;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionInputElementsQueryJsGenerator(IQueryElementsAlwaysJsCodeGenerator alwaysGenerator) : IRootActionInputElementsQueryJsGenerator
{
    private const string RootElementObjectName = "document";
    private const int ZeroIndent = 0;

    public StringBuilder GenerateJs(StringBuilder jsBuilder, IEnumerable<JsElementObjectName> jsInputObjectNames)
        => jsBuilder.Add(alwaysGenerator.GenerateJsCode, jsInputObjectNames, RootElementObjectName, ZeroIndent);
}
