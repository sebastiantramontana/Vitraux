using Vitraux.Modeling.Data.Actions;

namespace Vitraux.Execution.Building;

internal interface IViewModelActionsBuilder<TViewModel>
{
    public Task Build(string vmKey, ConfigurationBehavior configurationBehavior, IEnumerable<ActionData> actions);
}
