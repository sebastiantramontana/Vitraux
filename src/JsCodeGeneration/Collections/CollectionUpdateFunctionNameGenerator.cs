using Vitraux.Helpers;

namespace Vitraux.JsCodeGeneration.Collections;

internal class CollectionUpdateFunctionNameGenerator(IAtomicAutoNumberGenerator atomicAutoNumberGenerator) : ICollectionUpdateFunctionNameGenerator
{
    const string FunctionNamePrefix = "uc";

    public string Generate()
        => $"{FunctionNamePrefix}{atomicAutoNumberGenerator.Next()}";
}