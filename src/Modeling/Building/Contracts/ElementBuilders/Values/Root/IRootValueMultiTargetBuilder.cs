namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

public interface IRootValueMultiTargetBuilder<TViewModel, TValue>
{
    IRootValueElementSelectorBuilder<TViewModel, TValue> ToElements { get; }
    IRootValueCustomJsBuilder<TViewModel, TValue> ToJsFunction(string jsFunction);
}
