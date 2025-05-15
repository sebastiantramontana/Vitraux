using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Collections;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCollectionByUriJsGenerator(IFetchElementCall fetchElementCall)
    : IQueryElementsDeclaringAlwaysCollectionByUriJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            UriInsertionSelectorUri selectorUri => GenerateJsById(jsObjectName.Name, selectorUri.Uri),
            UriInsertionSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };

    private string GenerateJsById(string jsObjectName, Uri uri)
        => $"const {jsObjectName} = {fetchElementCall.Generate(uri)};";
}