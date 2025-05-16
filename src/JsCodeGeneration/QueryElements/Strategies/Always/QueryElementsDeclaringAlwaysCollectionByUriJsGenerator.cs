using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCollectionByUriJsGenerator(
    IFetchElementCall fetchElementCall,
    INotImplementedSelector notImplementedSelector)
    : IQueryElementsDeclaringAlwaysCollectionByUriJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            UriInsertionSelectorUri selectorUri => GenerateJsById(jsObjectName.Name, selectorUri.Uri),
            UriInsertionSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsById(string jsObjectName, Uri uri)
        => $"const {jsObjectName} = {fetchElementCall.Generate(uri)};";
}