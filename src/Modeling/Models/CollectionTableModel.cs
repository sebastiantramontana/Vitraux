namespace Vitraux.Modeling.Models;

internal record class CollectionTableModel(Delegate CollectionFunc) : CollectionElementModel(CollectionFunc);
