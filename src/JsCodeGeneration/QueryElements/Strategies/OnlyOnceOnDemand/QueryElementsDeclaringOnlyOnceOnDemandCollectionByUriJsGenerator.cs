using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceOnDemand;

internal class QueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator(IGetFetchedElementCall getFetchedElementCall) 
    : IQueryElementsDeclaringOnlyOnceOnDemandCollectionByUriJsGenerator
{
    public string GenerateJsCode(string parentObjectName, JsObjectName jsObjectName)
        => jsObjectName.AssociatedSelector switch
        {
            InsertElementUriSelectorUri uriSelector => GenerateJsByUri(jsObjectName.Name, uriSelector),
            InsertElementUriSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {jsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };

    private string GenerateJsByUri(string jsObjectName, InsertElementUriSelectorUri uriSelector)
        => $"const {jsObjectName} = {getFetchedElementCall.Generate(uriSelector.Uri, jsObjectName)};";
}
