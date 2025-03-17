using Vitraux.Modeling.Building.CustomJs;
using Vitraux.Modeling.Building.ElementBuilders.Collections.ContainerElements;
using Vitraux.Modeling.Building.ElementBuilders.Collections.Tables;
using Vitraux.Modeling.Building.ElementBuilders.Values;
using Vitraux.Modeling.Building.ModelMappers;

namespace Vitraux.Modeling.Building.ElementBuilders.Collections;

public interface ICollectionPopulateFromBuilder<TItem, TViewModelBack>
{
    ICollectionModelMapper<TItem, TViewModelBack> FromTemplate(string id);
    ICollectionModelMapper<TItem, TViewModelBack> FromTemplate(Func<TItem, string> idFunc);
    ICollectionModelMapper<TItem, TViewModelBack> FromUri(Uri uri);
    ICollectionModelMapper<TItem, TViewModelBack> FromUri(Func<TItem, Uri> uriFunc);
}

public interface ICollectionModelMapper<TViewModel, TModelMapperBack>
{
    ICollectionValueTargetBuilder<TViewModel, TValue> MapValue<TValue>(Func<TViewModel, TValue> func);
    
    
    ICollectionTargetBuilder<TItem, TViewModel> MapCollection<TItem>(Func<TViewModel, IEnumerable<TItem>> func);

    //PROBAR TENER EndCollection ACA Y NO EN ICollectionFinallizable
    TModelMapperBack EndCollection { get; }

    internal ModelMappingData Data { get; }
}

public interface ICollectionValueTargetBuilder<TViewModel, TValue>
{
    ICollectionValueElementSelectorBuilder<TViewModel, TValue> ToElements { get; }
    ICustomJsBuilder<TViewModel, TValue> ToJs(string jsFunction);
    IValueFinallizable<TViewModel, TValue> ToOwnMapping { get; }
}

//public interface ICollectionOfCollectionTargetBuilder<TItem, TViewModelBack>
//{
//    ITableSelectorBuilder<TItem, TViewModelBack> ToTables { get; }
//    IContainerElementsSelectorBuilder<TItem, TViewModelBack> ToContainerElements { get; }
//}

