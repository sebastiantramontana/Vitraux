using System.Text;
using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.CustomJsGeneration;

internal interface ICustomJsJsGenerator
{
    StringBuilder Generate(StringBuilder jsBuilder, CustomJsTargetBase customJsTarget, string objArg, int indentCount);
}
