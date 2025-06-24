using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.Execution.ViewModelNames;
internal interface IViewModelJsNamesMapper
{
    ViewModelJsNames MapFromFull(FullObjectNames fullObjectNames);
}