namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionParameterNameBuilder<TViewModel>
{
    IRootActionAddParametersFromBuilder<TViewModel> WithName(string paramName);
}
