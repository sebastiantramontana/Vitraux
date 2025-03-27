using Vitraux.Modeling.Data.Collections;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionNamesGenerator
{
    IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionElementModel> collections);
}
