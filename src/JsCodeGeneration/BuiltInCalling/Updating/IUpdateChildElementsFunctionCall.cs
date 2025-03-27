using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateChildElementsFunctionCall
{
    string Generate(ElementTarget toChildTargetElement, string parentValueObjectName, string valueObjectName);
}

