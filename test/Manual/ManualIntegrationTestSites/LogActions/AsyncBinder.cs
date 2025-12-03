using Vitraux;

namespace LogActions;

public class AsyncBinder : ActionParametersBinderAsyncBase<LogViewModel>
{
    public override Task BindActionAsync(LogViewModel viewModel, IDictionary<string, IEnumerable<string>> parameters)
    {
        var num1 = double.Parse(parameters["numbers"].ElementAt(0));
        var num2 = double.Parse(parameters["numbers"].ElementAt(1));
        var text = parameters["extratext"].Single();

        return viewModel.AddLogAsync(num1, num2, text);
    }
}