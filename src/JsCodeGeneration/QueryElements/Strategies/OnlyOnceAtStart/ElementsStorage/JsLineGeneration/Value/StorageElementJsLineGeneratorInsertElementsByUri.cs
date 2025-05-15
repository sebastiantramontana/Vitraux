using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Data.Selectors.Values.Insertions;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OnlyOnceAtStart.ElementsStorage.JsLineGeneration.Value;

internal class StorageElementJsLineGeneratorInsertElementsByUri(IStorageElementJsLineGeneratorByUri lineGeneratorByUri)
    : IStorageElementJsLineGeneratorInsertElementsByUri
{
    public string Generate(JsObjectName fetchedJsObjectName)
        => fetchedJsObjectName.AssociatedSelector switch
        {
            InsertElementUriSelectorUri uriSelector => lineGeneratorByUri.Generate(uriSelector.Uri, fetchedJsObjectName.Name),
            InsertElementUriSelectorDelegate => string.Empty,
            _ => throw new NotImplementedException($"Selector type {fetchedJsObjectName.AssociatedSelector} not implemented in {GetType().FullName}"),
        };
}
