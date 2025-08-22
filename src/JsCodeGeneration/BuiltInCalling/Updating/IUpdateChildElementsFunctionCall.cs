using Vitraux.Modeling.Data;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateChildElementsFunctionCall
{
    string Generate(ElementPlace childElementsPlace, string parentValueObjectName, string valueObjectName);
}

