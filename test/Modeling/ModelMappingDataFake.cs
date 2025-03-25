using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling
{
    internal class ModelMappingDataFake(IEnumerable<ValueModel> values, IEnumerable<CollectionElementModel> collectionElements) : ModelMappingData
    {
        public QueryElementStrategy QueryElementStrategy { get; set; }
        public bool TrackChanges { get; set; }
        IEnumerable<ValueModel> ModelMappingData.Values { get; } = values;
        IEnumerable<CollectionElementModel> ModelMappingData.CollectionElements { get; } = collectionElements;
    }
}
