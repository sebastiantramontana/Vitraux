using System.Text;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsValueJsGenerator
{
    StringBuilder GenerateJs(StringBuilder jsBuilder, string parentValueObjectName, ValueObjectNameWithJsTargets valueObjectNameWithTargets, int indentCount);
}
