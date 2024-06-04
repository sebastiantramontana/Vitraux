using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryTemplateCallingJsBuiltInFunctionCodeGenerator(
    IQueryAppendToElementsDeclaringByTemplateJsCodeGenerator appendToDeclaringGenerator)
    : IQueryTemplateCallingJsBuiltInFunctionCodeGenerator
{
    public string GenerateJsCode(ElementObjectName elementObjectName, Func<string> getElementTemplateCallingFunc, IJsQueryFromTemplateElementsDeclaringGeneratorContext queryGeneratorContext)
    {
        var templateObjectName = elementObjectName as ElementTemplateObjectName;
        var templateSelector = templateObjectName!.AssociatedSelector as ElementTemplateSelectorString;

        var templateDeclaring = $"const {templateObjectName.Name} = {getElementTemplateCallingFunc()};";
        var appendToDeclaring = appendToDeclaringGenerator.GenerateAppendToJsCode(templateObjectName.AppendToName, templateSelector!.ElementToAppend, queryGeneratorContext);

        return new StringBuilder()
            .AppendLine(templateDeclaring)
            .Append(appendToDeclaring)
            .ToString();
    }
}
