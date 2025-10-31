using System.Text;

namespace Vitraux.JsCodeGeneration.BuiltInCalling.Updating;

internal interface IUpdateValueByInsertingElementsCall
{
    StringBuilder Generate(StringBuilder jsBuilder, string elementToInsertObjectName, string appendToElemenstObjectName, string queryChildrenFunctionCall, string updateChildElementsFunctionCall, int indentCount);
}
