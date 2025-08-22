namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionAddParametersSourceBuilder<TViewModel>
{
    IRootActionParameterFromInputBuilder<TViewModel> FromInputs { get; }
    IRootActionParameterFromElementBuilder<TViewModel> FromElements { get; }
}
