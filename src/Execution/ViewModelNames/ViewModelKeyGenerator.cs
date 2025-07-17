namespace Vitraux.Execution.ViewModelNames;

internal class ViewModelKeyGenerator : IViewModelKeyGenerator
{
    public string Generate(Type vmType)
        => vmType.FullName!.Replace('.', '-');

    public string Generate<TViewModel>()
        => Generate(typeof(TViewModel));
}