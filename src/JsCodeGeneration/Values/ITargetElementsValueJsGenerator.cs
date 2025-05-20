using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsValueJsGenerator
{
    string GenerateJs(string parentValueObjectName, ValueObjectName valueObjectName, IEnumerable<JsObjectName> jsObjectNames);
}
