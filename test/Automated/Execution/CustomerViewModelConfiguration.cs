namespace Vitraux.Test.Execution;

internal class CustomerViewModelConfiguration : IModelConfiguration<CustomerViewModel>
{
    public ConfigurationBehavior ConfigurationBehavior { get; } = new()
    {
        QueryElementStrategy = QueryElementStrategy.OnlyOnceOnDemand,
        TrackChanges = true,
        VMUpdateFunctionCaching = VMUpdateFunctionCaching.ByVersion("test 1.0")
    };

    public ModelMappingData ConfigureMapping(IModelMapper<CustomerViewModel> modelMapper)
        => modelMapper
            .MapValue(c => c.FirstName).ToElements.ById("customer-firstname").ToContent
            .MapValue(c => c.MiddleName).ToElements.ById("customer-middlename").ToContent
            .MapValue(c => c.LastName).ToElements.ById("customer-lastname").ToContent
            .MapValue(c => c.Email).ToElements.ById("customer-email").ToContent
            .MapValue(c => c.BirthDate).ToElements.ById("customer-birthdate").ToContent
            .MapValue(c => c.Age).ToElements.ById("customer-age").ToContent
            .MapValue(c => GetHappyBirthdayCssClass(c.BirthDate)).ToElements.ById("customer-birthdate").ToAttribute("class")
            .MapValue(c => c.Balance).ToElements.ById("customer-balance").ToContent
            .MapValue(c => c.IsActive).ToElements.ById("customer-isactive-check").ToAttribute("checked")
            .Data;

    private static string GetHappyBirthdayCssClass(DateTime birthDate)
        => birthDate.DayOfYear == DateTime.Now.DayOfYear
            ? "happy-birthday"
            : "no-birthday";
}
