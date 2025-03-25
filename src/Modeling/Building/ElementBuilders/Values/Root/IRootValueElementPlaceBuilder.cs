using Vitraux.Modeling.Building.ElementBuilders.Values.Root.Insertions;

namespace Vitraux.Modeling.Building.ElementBuilders.Values.Root;

public interface IRootValueElementPlaceBuilder<TViewModel, TValue>
{
    IRootInsertFromBuilder<TViewModel, TValue> Insert { get; }
    IRootValueFinallizable<TViewModel, TValue> ToContent { get; }
    IRootValueFinallizable<TViewModel, TValue> ToAttribute(string attribute);
}
