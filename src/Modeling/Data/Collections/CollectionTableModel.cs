namespace Vitraux.Modeling.Data.Collections;

internal record class CollectionTableModel(Delegate CollectionFunc) : CollectionElementModel(CollectionFunc);
