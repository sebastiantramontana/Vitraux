using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Data.Collections;
using Vitraux.Modeling.Data.Values;

namespace Vitraux.Test.Modeling;

internal class ModelMappingDataFake(IEnumerable<ValueData> values, IEnumerable<CollectionData> collectionElements) : ModelMappingData
{
    public QueryElementStrategy QueryElementStrategy { get; set; }
    public bool TrackChanges { get; set; }
    IEnumerable<ValueData> ModelMappingData.Values { get; } = values;
    IEnumerable<CollectionData> ModelMappingData.CollectionElements { get; } = collectionElements;
}
