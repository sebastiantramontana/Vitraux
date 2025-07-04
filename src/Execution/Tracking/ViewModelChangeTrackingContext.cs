namespace Vitraux.Execution.Tracking;

internal class ViewModelChangeTrackingContext<TViewModel>(IViewModelNoChangesTracker<TViewModel> noChangesTracker, IViewModelShallowChangesTracker<TViewModel> shallowChangesTracker) : IViewModelChangeTrackingContext<TViewModel>
{
    public IViewModelChangesTracker<TViewModel> GetChangesTracker(bool trackChanges)
        => trackChanges ? shallowChangesTracker : noChangesTracker;
}
