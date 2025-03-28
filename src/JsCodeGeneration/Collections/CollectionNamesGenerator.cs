using Vitraux.Helpers;
using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionNamesGenerator : ICollectionNamesGenerator
{
    public IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionData> collections)
        => collections
            .Select((col, indexAsPostfix) => new CollectionObjectName($"collection{indexAsPostfix}", col))
            .RunNow();
}
