namespace Vitraux.Modeling.Data.Values;

internal record class HtmlElementPlace : ElementPlace
{
    internal static HtmlElementPlace Instance { get; } = new();

    private HtmlElementPlace() { }
}
