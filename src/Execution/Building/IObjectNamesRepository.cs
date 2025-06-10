using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.Execution.Building;

internal interface IObjectNamesRepository<TViewModel>
{
    public ViewModelSerializationData ViewModelSerializationData { get; set; }
}
