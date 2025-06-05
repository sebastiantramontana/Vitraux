namespace Vitraux.Test.Execution;

internal class CustomerViewModel
{
    public string FirstName { get; set; } = string.Empty;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; } = null;
    public DateTime BirthDate { get; set; }
    public double Balance { get; set; }
    public bool IsActive { get; set; } = true;
    public byte Age => BirthDate == default ? (byte)0 : (byte)(DateTime.Now.Year - BirthDate.Year - (DateTime.Now.DayOfYear < BirthDate.DayOfYear ? 1 : 0));
}
