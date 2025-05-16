using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysValueByUriJsGenerator(
    IFetchElementCall fetchElementCall,
    INotImplementedSelector notImplementedSelector)
    : IQueryElementsDeclaringAlwaysValueByUriJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            InsertElementUriSelectorUri uriSelector => GenerateJsByUri(jsObjectName.Name, uriSelector.Uri),
            InsertElementUriSelectorDelegate => string.Empty,
            _ => notImplementedSelector.ThrowNotImplementedException<string>(jsObjectName.AssociatedSelector)
        };

    private string GenerateJsByUri(string jsObjectName, Uri uri)
        => $"const {jsObjectName} = {fetchElementCall.Generate(uri)};";
}