using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.JsCodeGeneration.Actions;

internal interface IStorageElementActionJsLineGenerator
{
    string Generate(JsElementObjectName jsElementObjectName);
}
