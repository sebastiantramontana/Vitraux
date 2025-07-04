using Vitraux.Execution.Tracking;

namespace Vitraux.Execution.Serialization;

internal interface IViewModelJsonSerializer
{
    Task<string> Serialize(EncodedTrackedViewModelAllData encodedTrackedViewModelAllData);
}