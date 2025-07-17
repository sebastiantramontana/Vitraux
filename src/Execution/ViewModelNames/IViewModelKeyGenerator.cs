namespace Vitraux.Execution.ViewModelNames;

internal interface IViewModelKeyGenerator
{
    string Generate(Type type);
    string Generate<TViewModel>();
}
