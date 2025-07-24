namespace Vitraux;

public class VitrauxConfiguration
{
    public static VitrauxConfiguration Default { get; } = new VitrauxConfiguration();
    public bool UseShadowDom { get; set; } = true;
}
