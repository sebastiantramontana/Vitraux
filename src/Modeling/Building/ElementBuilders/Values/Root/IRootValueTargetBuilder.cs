namespace Vitraux.Modeling.Building.ElementBuilders.Values.Root;

public interface IRootValueTargetBuilder<TViewModel, TValue>
{
    IRootValueElementSelectorBuilder<TViewModel, TValue> ToElements { get; }
    IRootValueCustomJsBuilder<TViewModel, TValue> ToJsFunction(string jsFunction);
    IRootValueFinallizable<TViewModel, TValue> ToOwnMapping { get; }
}
