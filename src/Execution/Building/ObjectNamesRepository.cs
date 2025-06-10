using Vitraux.JsCodeGeneration.UpdateViews;

namespace Vitraux.Execution.Building;

internal class ObjectNamesRepository<TViewModel> : IObjectNamesRepository<TViewModel>
{
    public ViewModelSerializationData ViewModelSerializationData { get; set; } = default!;
}
