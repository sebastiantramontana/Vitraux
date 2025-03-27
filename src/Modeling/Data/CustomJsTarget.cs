namespace Vitraux.Modeling.Data;

internal record class CustomJsTarget<T>(string FunctionName) : Target<T> where T : Target<T>
{
    internal Uri? ModuleFrom { get; set; }
}
