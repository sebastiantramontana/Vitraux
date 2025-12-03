using Vitraux;

namespace LogActions;

public class SyncBinder : ActionParametersBinderBase<LogViewModel>
{
    public override void BindAction(LogViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        var num1 = double.Parse(parameters["numbers"].ElementAt(0));
        var num2 = double.Parse(parameters["numbers"].ElementAt(1));
        var text = parameters["extratext"].Single();

        viewModel.AddLog(num1, num2, text);
    }
}
