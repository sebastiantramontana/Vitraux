using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ModelMappers;

internal record class ModelMappingData
{
    internal IEnumerable<ValueModel> Values { get; } = [];
    internal IEnumerable<CollectionElementModel> CollectionElements { get; } = [];
}
