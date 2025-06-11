namespace Vitraux;

public interface IViewlUpdater<TViewModel>
{
    Task Update(TViewModel viewModel);
}