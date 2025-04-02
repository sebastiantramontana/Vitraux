namespace Vitraux.Modeling.Data;

internal abstract record class CustomJsTargetBase(string FunctionName) : ITarget
{
    internal Uri? ModuleFrom { get; set; } = null;
}
