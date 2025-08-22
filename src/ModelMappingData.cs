using Vitraux.Modeling.Data.Actions;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux;

public record class ModelMappingData
{
    private readonly ICollection<ValueData> _values = [];
    private readonly ICollection<CollectionData> _collections = [];
    private readonly ICollection<ActionData> _actions = [];

    internal IEnumerable<ValueData> Values => _values;
    internal IEnumerable<CollectionData> Collections => _collections;
    internal IEnumerable<ActionData> Actions => _actions;

    internal void AddValue(ValueData value) => _values.Add(value);
    internal void AddCollection(CollectionData collection) => _collections.Add(collection);
    internal void AddAction(ActionData action) => _actions.Add(action);

}
