using Vitraux.Modeling.Building.ModelMappers;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.TableRows;

namespace Vitraux.Modeling.Models
{
    internal record class CollectionTableModel(Delegate CollectionFunc)
    {
        internal ElementSelector TableSelector { get; set; } = default!;
        internal RowSelector RowSelector { get; set; } = default!;
        internal IModelMappingData ModelMappingData { get; set; } = default!;
    }
}
