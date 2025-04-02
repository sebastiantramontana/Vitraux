using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root.Insertions;

public interface IRootChildrenPlaceBuilder<TViewModel, TValue>
{
    IRootValueFinallizable<TViewModel, TValue> ToContent { get; }
    IRootValueFinallizable<TViewModel, TValue> ToAttribute(string attribute);
}