namespace Vitraux.Execution.Serialization;

internal class ViewModelSerializationDataCache<TViewModel> : IViewModelSerializationDataCache<TViewModel>
{
    public string ViewModelKey { get; set; } = string.Empty;
    public EncodedViewModelSerializationData ViewModelSerializationData { get; set; } = default!;
}
