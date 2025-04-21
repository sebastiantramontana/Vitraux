namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionData(Delegate DataFunc) : DelegateDataBase<ICollectionTarget>(DataFunc);
