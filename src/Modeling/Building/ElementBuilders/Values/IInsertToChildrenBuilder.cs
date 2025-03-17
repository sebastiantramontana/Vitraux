namespace Vitraux.Modeling.Building.ElementBuilders.Values;

public interface IInsertToChildrenBuilder<TViewModel, TValue>
{
    IChildrenSelectorBuilder<TViewModel, TValue> ToChildren { get; }
}
