using Vitraux.JsCodeGeneration.Collections;
using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Values;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    public IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionElementModel> collections)
        => collections.Select((col, indexAsPostfix) => new CollectionObjectName($"collection{indexAsPostfix}", col));
}
