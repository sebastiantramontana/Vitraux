namespace Vitraux;

public interface IViewlUpdater<TViewModel>
{
    void Update(TViewModel viewModel);
}