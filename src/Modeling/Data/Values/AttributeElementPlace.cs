namespace Vitraux.Modeling.Data.Values;

internal record class AttributeElementPlace : ElementPlace
{
    public AttributeElementPlace(string attribute)
        : base(ElementPlacing.Attribute, attribute)
    {
    }
}
