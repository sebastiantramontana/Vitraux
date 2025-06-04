using Microsoft.Extensions.DependencyInjection;
using Vitraux.Test.Example;

namespace Vitraux.Test.Execution;

public class ExecutionTest
{
    public void PetownerTest()
    {
        var container = new ServiceCollection();

        container.AddVitraux()
            .AddModelConfiguration<PetOwner, PetOwnerConfiguration>();
    }
}
