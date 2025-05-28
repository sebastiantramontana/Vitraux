namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionUpdateFunctionNameGenerator : ICollectionUpdateFunctionNameGenerator
{
    private int _counter = -1;
    const string FunctionNamePrefix = "uc";

    public string Generate()
        => $"{FunctionNamePrefix}{Interlocked.Increment(ref _counter)}";
}