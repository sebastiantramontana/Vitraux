using Vitraux.Execution.Serialization;

namespace Vitraux.Execution.Building;

internal class ViewModelSerializationDataCache<TViewModel> : IViewModelSerializationDataCache<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public EncodedViewModelSerializationData ViewModelSerializationData { get; set; } = default!;
}
