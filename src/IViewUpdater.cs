namespace Vitraux;

public interface IViewUpdater<TViewModel> where TViewModel : class
{
    Task Update();
    Task Update(TViewModel viewModel);
}