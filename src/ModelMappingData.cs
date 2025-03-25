using Vitraux.Modeling.Models;

namespace Vitraux;

public record class ModelMappingData
{
    internal IEnumerable<ValueModel> Values { get; } = [];
    internal IEnumerable<CollectionElementModel> CollectionElements { get; } = [];
}
