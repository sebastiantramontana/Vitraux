using Vitraux.Execution.JsInvokers;

namespace Vitraux.Execution.Building;

internal class ConfigurationBuilder(
    IJsConfigureInvoker jsConfigureInvoker,
    VitrauxConfiguration vitrauxConfiguration) : IBuilder
{
    public Task Build()
    {
        jsConfigureInvoker.Invoke(vitrauxConfiguration.UseShadowDom);
        return Task.CompletedTask;
    }
}
