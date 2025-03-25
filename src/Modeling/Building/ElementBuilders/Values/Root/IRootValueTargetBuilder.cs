namespace Vitraux.Modeling.Building.ElementBuilders.Values.Root;

public interface IRootValueTargetBuilder<TViewModel, TValue> : IRootValueMultiTargetBuilder<TViewModel, TValue>
{
    IModelMapper<TViewModel> ToOwnMapping { get; }
}