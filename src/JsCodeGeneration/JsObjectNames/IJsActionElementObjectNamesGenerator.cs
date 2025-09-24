using Vitraux.Modeling.Data.Actions;

namespace Vitraux.JsCodeGeneration.JsObjectNames;

internal interface IJsActionElementObjectNamesGenerator
{
    public IEnumerable<JsElementObjectName> Generate(string namePrefix, IEnumerable<ActionData> actions);
}
