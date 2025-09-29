namespace Vitraux.Test.Example;

public record class PetOwner(string Name, string Address, string PhoneNumber, string HtmlComments, Subscription Subscription, IEnumerable<Pet> Pets)
{
    public void Method1() { }
    public Task Method2() => Task.CompletedTask;
}