namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionPopulateFromBuilder<TItem, TEndCollectionReturn>
{
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(string id);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromTemplate(Func<TItem, string> idFunc);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Uri uri);
    ICollectionModelMapper<TItem, TEndCollectionReturn> FromUri(Func<TItem, Uri> uriFunc);
}

public interface ICollectionModelMapper<TItem, TEndCollectionReturn>
{
    ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn> MapValue<TValue>(Func<TItem, TValue> func);
    ICollectionTargetBuilder<TInnerItem, IInnerCollectionFinallizable<TItem, TEndCollectionReturn>> MapCollection<TInnerItem>(Func<TItem, IEnumerable<TInnerItem>> func);
    internal ModelMappingData Data { get; }
}

public interface IInnerCollectionFinallizable<TItemBack, TEndCollectionReturn> : ICollectionModelMapper<TItemBack, TEndCollectionReturn>
{
    TEndCollectionReturn EndCollection { get; }
}

public interface ICollectionValueTargetBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn> ToElements { get; }
    ICollectionValueCustomJsBuilder<TItem, TValue, TEndCollectionReturn> ToJsFunction(string jsFunction);
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToOwnMapping { get; }
}

public interface ICollectionValueElementSelectorBuilder<TItem, TValue, TEndCollectionReturn>
{
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(string id);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(Func<TValue, string> idFunc);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ById(Func<TItem, string> idFunc);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(string query);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(Func<TValue, string> queryFunc);
    ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn> ByQuery(Func<TItem, string> queryFunc);
}

public interface ICollectionValueElementPlaceBuilder<TItem, TValue, TEndCollectionReturn>
{
    //IRootInsertFromBuilder<TItem, TValue> Insert { get; }
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToContent { get; }
    ICollectionValueFinallizable<TItem, TValue, TEndCollectionReturn> ToAttribute(string attribute);
}
