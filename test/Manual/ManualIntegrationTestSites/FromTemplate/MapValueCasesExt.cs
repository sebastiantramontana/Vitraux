using Vitraux;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Collections.CollectionValues;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace FromTemplate;

public static class CommonConstants
{
    public const string NonExistentElement = "non-existent-element";
    public const string NonExistentElements = "[non-existent-elements]";
    public const string NonExistentChildren = "[non-existent-children]";
    public const string TimeChildren = "[time-children]";
    public const string NonExistentTemplate = "non-existent-template";
    public const string OneChildTemplate = "one-child-template";
    public const string ManyChildrenTemplate = "many-children-template";
}

public static class MapCollectionCasesExt
{
    private static Delegate _func = default!;

    public static IRootCollectionTargetBuilder<TItem, TViewModel> MapTestCollection<TViewModel, TItem>(this IModelMapper<TViewModel> mapper, Func<TViewModel, IEnumerable<TItem>> func)
    {
        _func = func;
        return mapper.MapCollection(func);
    }

    public static IRootCollectionTargetBuilder<TItem, TViewModel> MapTestCollection<TValue, TViewModel, TItem>(this IRootValueFinallizable<TViewModel, TValue> mapper, Func<TViewModel, IEnumerable<TItem>> func)
        => MapTestCollection(mapper, func);

    public static IRootCollectionTargetBuilder<TItem, TViewModel> MapCollectionLalala2<TViewModel, TItem>(this IRootCollectionTargetBuilder<TItem, TViewModel> builder)
        => builder
            .ToContainerElements.ById("non-existent-container-numbers").FromTemplate("numbers-for-container-template")
                    .MapValue(n => n).ToElements.ByQuery("non-existent-shuffled-number").ToContent
                    .MapCollection(n=>n.ToString()!).ToTables.ByQuery("dfdfdf").PopulatingRows.FromTemplate("dfdfsf").
            .EndCollection
            .MapCollection((_func as Func<TViewModel, IEnumerable<TItem>>)!);

    private static IRootCollectionTargetBuilder<TItem, TViewModel> MapById<TViewModel, TItem>(this IRootCollectionTargetBuilder<TItem, TViewModel> builder, string containerId, string tableId, string templateId, string childValueQuery)
        => builder
                .ToContainerElements.ById(containerId).FromTemplate(templateId)
                    .MapChildValue(childValueQuery)
            .EndCollection
            .MapCollection((_func as Func<TViewModel, IEnumerable<TItem>>)!)
                .ToTables.ById(tableId).PopulatingRows.FromTemplate(templateId)
                    .MapChildValue(childValueQuery)
            .EndCollection
            .MapCollection<TViewModel, TItem>();

    private static ICollectionValueFinallizable<TItem, TItem, IModelMapper<TViewModel>> MapChildValue<TItem, TViewModel>(this ICollectionModelMapper<TItem, IModelMapper<TViewModel>> mapper, string childValueQuery)
        => mapper.MapValue(n => n).ToElements.ByQuery(childValueQuery).ToContent;

    private static IRootCollectionTargetBuilder<TItem, TViewModel> MapCollection<TViewModel, TItem>(this IModelMapper<TViewModel> mapper)
        => mapper.MapCollection((_func as Func<TViewModel, IEnumerable<TItem>>)!);
}

public static class MapValueCasesExt
{
    private const string OneElementForZeroChildreNonExistentTemplate = "element-for-zero-children-non-existen-template";
    private const string OneElementForZeroChidrenTemplateOne = "element-for-zero-children-template-one";
    private const string OneElementForZeroChidrenTemplateMany = "element-for-zero-children-template-many";
    private const string ManyElementsForZeroChildrenNonExistentTemplate = "[many-elements-for-zero-children-non-existent-template]";
    private const string ManyElementsForZeroChidrenTemplateOne = "[many-elements-for-zero-children-template-one]";
    private const string ManyElementsForZeroChidrenTemplateMany = "[many-elements-for-zero-children-template-many]";

    private const string OneElementForChildrenNonExistentTemplate = "element-for-children-non-existent-template";
    private const string OneElementForOneChidren = "element-for-one-child";
    private const string ManyElementsForChildrenNonExistentTemplate = "[many-elements-for-children-non-existent-template]";
    private const string ManyElementsForOneChidren = "[many-elements-for-one-child]";
    private const string OneElementForManyChidren = "element-for-many-children";
    private const string ManyElementsForManyChidren = "[many-elements-for-many-children]";

    public static IRootValueFinallizable<FromTemplateViewModel, TValue> MapValueZeroToZeroCases<TValue>(this IRootValueTargetBuilder<FromTemplateViewModel, TValue> builder)
        => builder
            .MapById(CommonConstants.NonExistentElement, CommonConstants.NonExistentTemplate, CommonConstants.NonExistentChildren)
            .MapByQuery(CommonConstants.NonExistentElements, CommonConstants.NonExistentTemplate, CommonConstants.NonExistentChildren)
            .MapById(CommonConstants.NonExistentElement, CommonConstants.OneChildTemplate, CommonConstants.NonExistentChildren)
            .MapByQuery(CommonConstants.NonExistentElements, CommonConstants.OneChildTemplate, CommonConstants.NonExistentChildren)
            .MapById(CommonConstants.NonExistentElement, CommonConstants.ManyChildrenTemplate, CommonConstants.NonExistentChildren)
            .MapByQuery(CommonConstants.NonExistentElements, CommonConstants.ManyChildrenTemplate, CommonConstants.NonExistentChildren);

    public static IRootValueFinallizable<FromTemplateViewModel, TValue> MapValueOneOrManyToZeroCases<TValue>(this IRootValueFinallizable<FromTemplateViewModel, TValue> builder)
        => builder
            .MapById(OneElementForZeroChildreNonExistentTemplate, CommonConstants.NonExistentTemplate, CommonConstants.NonExistentChildren)
            .MapByQuery(ManyElementsForZeroChildrenNonExistentTemplate, CommonConstants.NonExistentTemplate, CommonConstants.NonExistentChildren)
            .MapById(OneElementForZeroChidrenTemplateOne, CommonConstants.OneChildTemplate, CommonConstants.NonExistentChildren)
            .MapByQuery(ManyElementsForZeroChidrenTemplateOne, CommonConstants.OneChildTemplate, CommonConstants.NonExistentChildren)
            .MapById(OneElementForZeroChidrenTemplateMany, CommonConstants.ManyChildrenTemplate, CommonConstants.NonExistentChildren)
            .MapByQuery(ManyElementsForZeroChidrenTemplateMany, CommonConstants.ManyChildrenTemplate, CommonConstants.NonExistentChildren);

    public static IRootValueFinallizable<FromTemplateViewModel, TValue> MapValueOneOrManyToOneOrManyCases<TValue>(this IRootValueFinallizable<FromTemplateViewModel, TValue> builder)
        => builder
            .MapById(OneElementForChildrenNonExistentTemplate, CommonConstants.NonExistentTemplate, CommonConstants.TimeChildren)
            .MapByQuery(ManyElementsForChildrenNonExistentTemplate, CommonConstants.NonExistentTemplate, CommonConstants.TimeChildren)
            .MapById(OneElementForOneChidren, CommonConstants.OneChildTemplate, CommonConstants.TimeChildren)
            .MapByQuery(ManyElementsForOneChidren, CommonConstants.OneChildTemplate, CommonConstants.TimeChildren)
            .MapById(OneElementForManyChidren, CommonConstants.ManyChildrenTemplate, CommonConstants.TimeChildren)
            .MapByQuery(ManyElementsForManyChidren, CommonConstants.ManyChildrenTemplate, CommonConstants.TimeChildren);

    private static IRootValueFinallizable<FromTemplateViewModel, TValue> MapById<TValue>(this IRootValueTargetBuilder<FromTemplateViewModel, TValue> builder, string elementId, string templateId, string childrenQuery)
        => builder.ToElements.ByIdFromTemplate(elementId, templateId, childrenQuery);

    private static IRootValueFinallizable<FromTemplateViewModel, TValue> MapById<TValue>(this IRootValueFinallizable<FromTemplateViewModel, TValue> builder, string elementId, string templateId, string childrenQuery)
        => builder.ToElements.ByIdFromTemplate(elementId, templateId, childrenQuery);

    private static IRootValueFinallizable<FromTemplateViewModel, TValue> ByIdFromTemplate<TValue>(this IRootValueElementSelectorBuilder<FromTemplateViewModel, TValue> toElements, string elementId, string templateId, string childrenQuery)
        => toElements.ById(elementId).Insert.FromTemplate(templateId).ToChildren.ByQuery(childrenQuery).ToContent;

    private static IRootValueFinallizable<FromTemplateViewModel, TValue> MapByQuery<TValue>(this IRootValueFinallizable<FromTemplateViewModel, TValue> builder, string elementQuery, string templateId, string childrenQuery)
        => builder.ToElements.ByQuery(elementQuery).Insert.FromTemplate(templateId).ToChildren.ByQuery(childrenQuery).ToContent;
}
