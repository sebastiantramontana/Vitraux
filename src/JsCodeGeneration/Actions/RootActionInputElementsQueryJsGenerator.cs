using System.Text;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

namespace Vitraux.JsCodeGeneration.Actions;

internal class RootActionInputElementsQueryJsGenerator(IQueryElementsAlwaysJsCodeGenerator alwaysGenerator) : IRootActionInputElementsQueryJsGenerator
{
    public StringBuilder GenerateJs(StringBuilder stringBuilder, IEnumerable<JsElementObjectName> jsInputObjectNames)
        => stringBuilder.AppendLine(alwaysGenerator.GenerateJsCode(jsInputObjectNames, string.Empty));
}
