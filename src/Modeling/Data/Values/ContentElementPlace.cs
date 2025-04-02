namespace Vitraux.Modeling.Data.Values;

internal record class ContentElementPlace : ElementPlace
{
    internal static ContentElementPlace Instance { get; } = new();

    private ContentElementPlace() { }
}
