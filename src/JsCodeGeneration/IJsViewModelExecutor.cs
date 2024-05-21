namespace Vitraux.JsCodeGeneration
{
    internal interface IJsViewModelExecutor<TViewModel>
    {
        void Execute(TViewModel viewModel);
    }
}
