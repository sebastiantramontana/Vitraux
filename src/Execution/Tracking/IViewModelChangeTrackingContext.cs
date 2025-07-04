namespace Vitraux.Execution.Tracking;

internal interface IViewModelChangeTrackingContext<TViewModel>
{
    IViewModelChangesTracker<TViewModel> GetChangesTracker(bool trackChanges);
}
