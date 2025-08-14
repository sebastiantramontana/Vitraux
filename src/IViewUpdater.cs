namespace Vitraux;

public interface IViewUpdater<TViewModel>
{
    Task Update(TViewModel viewModel);
}