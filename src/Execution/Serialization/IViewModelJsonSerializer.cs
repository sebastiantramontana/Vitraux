using Vitraux.Execution.Tracking.Encoded;

namespace Vitraux.Execution.Serialization;

internal interface IViewModelJsonSerializer
{
    Task<string> Serialize(EncodedTrackedViewModelJsAllData encodedTrackedViewModelAllData);
}