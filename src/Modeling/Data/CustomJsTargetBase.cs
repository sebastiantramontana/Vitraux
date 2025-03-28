namespace Vitraux.Modeling.Data;

internal abstract record class CustomJsTargetBase<T>(string FunctionName) : Target<T> where T : Target<T>
{
    internal Uri? ModuleFrom { get; set; } = null;
}
