namespace Vitraux;

public interface IViewUpdater<TViewModel> where TViewModel : notnull
{
    Task Update(TViewModel viewModel);
}