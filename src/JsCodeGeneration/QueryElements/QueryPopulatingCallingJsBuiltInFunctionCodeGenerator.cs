using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryPopulatingCallingJsBuiltInFunctionCodeGenerator(
    IQueryAppendToElementsDeclaringByPopulatingJsCodeGenerator appendToDeclaringGenerator)
    : IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator
{
    public string GenerateJsCode(ElementObjectName elementObjectName, string getPopulatingElementsCallingJsCode, IJsQueryPopulatingElementsDeclaringGeneratorContext queryGeneratorContext)
    {
        var populatingElementObjectName = elementObjectName as InsertElementObjectName;
        var populatingSelector = populatingElementObjectName!.AssociatedSelector as InsertElementSelectorBase;

        var populatingDeclaring = $"const {populatingElementObjectName.JsObjName} = {getPopulatingElementsCallingJsCode};";
        var appendToDeclaring = appendToDeclaringGenerator.GenerateAppendToJsCode(populatingElementObjectName.AppendToJsObjNameName, populatingSelector!.ElementToAppend, queryGeneratorContext);

        return new StringBuilder()
            .AppendLine(populatingDeclaring)
            .Append(appendToDeclaring)
            .ToString();
    }
}
