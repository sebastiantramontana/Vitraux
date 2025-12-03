namespace Vitraux.Execution;

internal class ViewModelRepository : IViewModelRepository
{
    private static readonly Dictionary<string, object> _vmInstances = [];

    public TViewModel GetViewModelInstance<TViewModel>(string vmKey) where TViewModel : class
        => GetViewModel(vmKey) as TViewModel ?? throw new InvalidOperationException($"Viewmodel instance with key {vmKey} exists, but it is not casteable to {typeof(TViewModel).Name}");

    public void SetViewModelInstance<TViewModel>(string vmKey, TViewModel viewModel) where TViewModel : class
        => _vmInstances[vmKey] = viewModel;

    internal static object GetViewModel(string vmKey)
        => _vmInstances.TryGetValue(vmKey, out var viewModel)
            ? viewModel
            : throw new InvalidOperationException($"Viewmodel instance with key {vmKey} not found. Probably, IViewUpdater<TViewModel>.Update(viewModel) was never called");
}
