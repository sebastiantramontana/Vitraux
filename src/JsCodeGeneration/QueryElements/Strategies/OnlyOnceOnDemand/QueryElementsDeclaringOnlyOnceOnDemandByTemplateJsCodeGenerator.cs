﻿using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator(
    IGetStoredElementByTemplateAsArrayCall getStoredElementByTemplateAsArrayCall,
    IQueryTemplateCallingJsBuiltInFunctionCodeGenerator queryElementsDeclaringByTemplateCallingJsBuilt,
    IJsQueryFromTemplateElementsDeclaringOnlyOnceOnDemandGeneratorFactory queryGeneratorFactory)
    : IQueryElementsDeclaringOnlyOnceOnDemandByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
        => queryElementsDeclaringByTemplateCallingJsBuilt.GenerateJsCode(elementObjectName, () => getStoredElementByTemplateAsArrayCall.Generate(elementObjectName.AssociatedSelector.Value, elementObjectName.Name), queryGeneratorFactory);
}