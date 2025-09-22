using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.Values;

internal interface ITargetElementsDirectUpdateValueJsGenerator
{
    string GenerateJs(string jsElementObjectName, ElementPlace place, string parentValueObjectName, string valueObjectName);
}
