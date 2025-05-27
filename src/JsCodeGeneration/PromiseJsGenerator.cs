namespace Vitraux.JsCodeGeneration;

internal class PromiseJsGenerator : IPromiseJsGenerator
{
    const string ReturnedResolvedPromise = "return Promise.resolve();";

    public string ReturnResolvedPromiseJsLine => ReturnedResolvedPromise;
}