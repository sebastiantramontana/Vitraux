namespace Vitraux.Modeling.Building.Contracts.ElementBuilders.Actions;

public interface IRootActionAddParameterNameBuilder<TViewModel>
{
    IRootActionAddParametersFromBuilder<TViewModel> AddParameter(string paramName);
}
