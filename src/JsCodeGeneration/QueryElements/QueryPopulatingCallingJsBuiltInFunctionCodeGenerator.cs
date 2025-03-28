using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryPopulatingCallingJsBuiltInFunctionCodeGenerator(
    IQueryAppendToElementsDeclaringByPopulatingJsCodeGenerator appendToDeclaringGenerator)
    : IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator
{
    public string GenerateJsCode(ElementObjectName elementObjectName, string getPopulatingElementsCallingJsCode, IJsQueryPopulatingElementsDeclaringGeneratorContext queryGeneratorContext)
    {
        var populatingElementObjectName = elementObjectName as PopulatingElementObjectName;
        var populatingSelector = populatingElementObjectName!.AssociatedSelector as PopulatingElementSelectorBase;

        var populatingDeclaring = $"const {populatingElementObjectName.Name} = {getPopulatingElementsCallingJsCode};";
        var appendToDeclaring = appendToDeclaringGenerator.GenerateAppendToJsCode(populatingElementObjectName.AppendToName, populatingSelector!.ElementToAppend, queryGeneratorContext);

        return new StringBuilder()
            .AppendLine(populatingDeclaring)
            .Append(appendToDeclaring)
            .ToString();
    }
}
