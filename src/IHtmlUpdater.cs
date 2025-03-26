namespace Vitraux;

public interface IHtmlUpdater<TViewModel>
{
    void Update(TViewModel viewModel);
}