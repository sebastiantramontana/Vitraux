namespace Vitraux.Test.Example;

public interface IPetOwnerActionParameterBinder1 : IActionParametersBinder<PetOwner> { }

public class PetOwnerActionParameterBinder1 : IPetOwnerActionParameterBinder1
{
    public void BindAction(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters) { }
}

public interface IPetOwnerActionParameterBinder2 : IActionParametersBinderAsync<PetOwner> { }

public class PetOwnerActionParameterBinder2 : IPetOwnerActionParameterBinder2
{
    public Task BindActionAsync(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters)
        => Task.CompletedTask;
}

public class PetOwnerActionParameterBinder3 : IActionParametersBinderAsync<PetOwner>
{
    public Task BindActionAsync(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters)
        => Task.CompletedTask;
}

public class PetOwnerActionParameterBinder4 : IActionParametersBinder<PetOwner>
{
    public void BindAction(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters) { }
}