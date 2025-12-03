using PetOwnerWasm.ViewModel;
using System.Diagnostics.CodeAnalysis;
using Vitraux;

namespace PetOwnerWasm.ModelConfigurations;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
internal class SelectPetOwnerBinder : ActionParametersBinderAsyncBase<AllPetOwnerNames>
{
    public override Task BindActionAsync(AllPetOwnerNames viewModel, IDictionary<string, IEnumerable<string>> parameters)
        => viewModel.SelectPetOwner(int.Parse(parameters["inputValue"].Single()));
}
