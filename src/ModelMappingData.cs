using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux;

public record class ModelMappingData
{
    internal IEnumerable<ValueModel> Values { get; } = [];
    internal IEnumerable<CollectionElementModel> CollectionElements { get; } = [];
}
