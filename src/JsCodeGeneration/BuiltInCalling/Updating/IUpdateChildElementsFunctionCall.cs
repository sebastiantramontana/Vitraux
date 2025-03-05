using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateChildElementsFunctionCall
{
    string Generate(TargetElement toChildTargetElement, string parentValueObjectName, string valueObjectName);
}

