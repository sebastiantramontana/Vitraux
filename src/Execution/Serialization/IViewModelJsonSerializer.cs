namespace Vitraux.Execution.Serialization;

internal interface IViewModelJsonSerializer
{
    Task<string> Serialize(EncodedViewModelSerializationData viewModelSerializationData, object viewModel);
}