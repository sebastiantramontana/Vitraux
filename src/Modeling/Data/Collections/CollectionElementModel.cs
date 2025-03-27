namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionElementModel(Delegate CollectionFunc)
{
    internal Target<CollectionTarget> Target { get; set; } = default!;
    internal ModelMappingData ModelMappingData { get; set; } = default!;
}
