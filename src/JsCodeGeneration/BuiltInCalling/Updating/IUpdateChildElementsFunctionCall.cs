using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateChildElementsFunctionCall
{
    string Generate(ElementPlace childElementsPlace, string parentValueObjectName, string valueObjectName);
}

