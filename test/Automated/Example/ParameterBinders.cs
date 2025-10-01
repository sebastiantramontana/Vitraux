namespace Vitraux.Test.Example;

public interface IPetOwnerActionParameterBinder1 : IActionParametersBinder<PetOwner> { }

public class PetOwnerActionParameterBinder1 : IPetOwnerActionParameterBinder1
{
    public void BindAction(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters) { }
}

public interface IPetOwnerActionParameterBinder2 : IActionParametersBinderAsync<PetOwner> { }

public class PetOwnerActionParameterBinder2 : IPetOwnerActionParameterBinder2
{
    public Task BindAction(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters)
        => Task.CompletedTask;
}

public interface IPetOwnerActionParameterBinder3 : IActionParametersBinderAsync<PetOwner> { }

public class PetOwnerActionParameterBinder3 : IPetOwnerActionParameterBinder3
{
    public Task BindAction(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters)
        => Task.CompletedTask;
}

public interface IPetOwnerActionParameterBinder4 : IActionParametersBinder<PetOwner> { }

public class PetOwnerActionParameterBinder4 : IPetOwnerActionParameterBinder4
{
    public void BindAction(PetOwner viewModel, IDictionary<string, IEnumerable<string>> parameters) { }
}