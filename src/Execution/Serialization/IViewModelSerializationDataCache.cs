namespace Vitraux.Execution.Serialization;

internal interface IViewModelSerializationDataCache<TViewModel>
{
    public string ViewModelKey { get; set; }
    public EncodedViewModelSerializationData ViewModelSerializationData { get; set; }
}
