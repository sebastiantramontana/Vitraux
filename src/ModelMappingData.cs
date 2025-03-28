using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux;

public record class ModelMappingData
{
    internal IEnumerable<ValueData> Values { get; } = [];
    internal IEnumerable<CollectionData> CollectionElements { get; } = [];
}
