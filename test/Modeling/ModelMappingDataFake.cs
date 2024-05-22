using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Models;

namespace Vitraux.Test.Modeling
{
    internal class ModelMappingDataFake(IEnumerable<ValueModel> values, IEnumerable<CollectionElementModel> collectionElements) : IModelMappingData
    {
        public QueryElementStrategy QueryElementStrategy { get; set; }
        public bool TrackChanges { get; set; }
        IEnumerable<ValueModel> IModelMappingData.Values { get; } = values;
        IEnumerable<CollectionElementModel> IModelMappingData.CollectionElements { get; } = collectionElements;
    }
}
