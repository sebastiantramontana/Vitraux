namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionData(Delegate CollectionFunc)
{
    internal CollectionTarget Target { get; set; } = default!;
    internal ModelMappingData ModelMappingData { get; set; } = default!;
}
