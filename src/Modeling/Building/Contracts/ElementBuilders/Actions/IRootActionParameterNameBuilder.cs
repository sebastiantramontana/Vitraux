namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterNameBuilder<TViewModel>
{
    IRootActionAddParametersSourceBuilder<TViewModel> WithName(string paramName);
}
