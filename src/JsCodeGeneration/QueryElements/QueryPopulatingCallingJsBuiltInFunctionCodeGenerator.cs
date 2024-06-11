﻿using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryPopulatingCallingJsBuiltInFunctionCodeGenerator(
    IQueryAppendToElementsDeclaringByPopulatingJsCodeGenerator appendToDeclaringGenerator)
    : IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator
{
    public string GenerateJsCode(ElementObjectName elementObjectName, Func<string> getPopulatingElementsCallingFunc, IJsQueryPopulatingElementsDeclaringGeneratorContext queryGeneratorContext)
    {
        var populatingElementObjectName = elementObjectName as PopulatingElementObjectName;
        var populatingSelector = populatingElementObjectName!.AssociatedSelector as PopulatingElementSelectorBase;

        var populatingDeclaring = $"const {populatingElementObjectName.Name} = {getPopulatingElementsCallingFunc()};";
        var appendToDeclaring = appendToDeclaringGenerator.GenerateAppendToJsCode(populatingElementObjectName.AppendToName, populatingSelector!.ElementToAppend, queryGeneratorContext);

        return new StringBuilder()
            .AppendLine(populatingDeclaring)
            .Append(appendToDeclaring)
            .ToString();
    }
}