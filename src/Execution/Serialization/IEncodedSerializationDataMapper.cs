using Vitraux.JsCodeGeneration.JsObjectNames;

namespace Vitraux.Execution.Serialization;
internal interface IEncodedSerializationDataMapper
{
    EncodedViewModelSerializationData MapToEncoded(FullObjectNames fullObjectNames);
}