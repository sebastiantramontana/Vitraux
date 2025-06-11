using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.Execution.Serialization;

internal interface ISerializationDataMapper
{
    EncodedViewModelSerializationData MapToEncoded(ViewModelSerializationData viewModelSerializationData);
}
