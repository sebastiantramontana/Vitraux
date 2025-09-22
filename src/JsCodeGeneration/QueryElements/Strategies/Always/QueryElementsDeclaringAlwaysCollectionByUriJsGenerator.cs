using Vitraux.Helpers;
using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.JsObjectNames;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCollectionByUriJsGenerator(
    IFetchElementCall fetchElementCall,
    INotImplementedCaseGuard notImplementedSelector)
    : IQueryElementsDeclaringAlwaysCollectionByUriJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsElementObjectName jsElementObjectName)
        => jsElementObjectName.AssociatedSelector switch
        {
            UriInsertionSelectorUri selectorUri => GenerateJsById(jsElementObjectName.Name, selectorUri.Uri),
            _ => notImplementedSelector.ThrowException<string>(jsElementObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsElementObjectName, Uri uri)
        => $"const {jsElementObjectName} = {fetchElementCall.Generate(uri)};";
}