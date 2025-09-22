using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator(
    IGetFetchedElementCall getFetchedElementCall,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringOnlyOnceOnDemandValueByUriJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            InsertElementUriSelectorUri uriSelector => GenerateJsByUri(jsElementObjectName.Name, uriSelector),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string GenerateJsByUri(string jsElementObjectName, InsertElementUriSelectorUri uriSelector)
        => $"const {jsElementObjectName} = {getFetchedElementCall.Generate(uriSelector.Uri, jsElementObjectName)};";
}