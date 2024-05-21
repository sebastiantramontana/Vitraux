using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building.ModelMappers;

public interface IModelMappingData
{
    internal IEnumerable<ValueModel> Values { get; }
    internal IEnumerable<CollectionTableModel> CollectionTables { get; }
}
