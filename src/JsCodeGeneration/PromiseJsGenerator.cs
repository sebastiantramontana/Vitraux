using System.Text;
using Vitraux.JsCodeGeneration.Formating;

namespace Vitraux.JsCodeGeneration;

internal class PromiseJsGenerator(ICodeFormatter codeFormatter) : IPromiseJsGenerator
{
    const string ReturnedResolvedPromise = "return Promise.resolve();";

    public string ReturnResolvedPromiseJsLine => ReturnedResolvedPromise;

    public StringBuilder GenerateJs(StringBuilder jsBuilder, int indentCount)
        => jsBuilder.Append(codeFormatter.IndentLine(ReturnedResolvedPromise, indentCount));
}