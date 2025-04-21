using Vitraux.Modeling.Data.Values;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateChildElementsFunctionCall
{
    string Generate(ElementValueTarget toChildTargetElement, string parentValueObjectName, string valueObjectName);
}

