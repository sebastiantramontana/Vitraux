using Vitraux.Execution.ViewModelNames;

namespace Vitraux.Execution.Serialization;

internal interface IViewModelJsonSerializer
{
    Task<string> Serialize(ViewModelJsNames viewModelSerializationData, object viewModel);
}