﻿using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements.Populating;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByFetchJsCodeGenerator(
    IFetchElementCall fetchElementCall,
    IQueryPopulatingCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByTemplateCallingJsBuilt,
    IJsQueryPopulatingElementsDeclaringAlwaysGeneratorContext queryGeneratorContext)
    : IQueryElementsDeclaringAlwaysByFetchJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByTemplateCallingJsBuilt.GenerateJsCode(elementObjectName, () => fetchElementCall.Generate((elementObjectName.AssociatedSelector as ElementFetchSelectorUri).Uri), queryGeneratorContext);
}