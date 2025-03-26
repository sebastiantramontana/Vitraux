namespace Vitraux.Modeling.Models;

internal record class AttributeElementPlace : ElementPlace
{
    public AttributeElementPlace(string attribute)
        : base(ElementPlacing.Attribute, attribute)
    {
    }
}
