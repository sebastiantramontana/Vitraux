using Vitraux.Modeling.Models;

namespace Vitraux.JsCodeGeneration.Collections;

internal interface ICollectionNamesGenerator
{
    IEnumerable<CollectionObjectName> Generate(IEnumerable<CollectionElementModel> collections);
}
