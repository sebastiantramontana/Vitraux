using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.CustomJsGeneration;

internal interface ICustomJsJsGenerator
{
    string Generate(CustomJsTargetBase customJsTarget, string objArg);
}
