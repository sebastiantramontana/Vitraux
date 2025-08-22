using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsDirectUpdateValueJsGenerator
{
    string GenerateJs(string jsObjectName, ElementPlace place, string parentValueObjectName, string valueObjectName);
}
