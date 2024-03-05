namespace Labb2_DbFirst_Template.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public int? StoreId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? JobTitle { get; set; }

    public DateOnly? EmployeeSince { get; set; }

    public string? ContactEMail { get; set; }

    public int? Phone { get; set; }

    public virtual Store? Store { get; set; }
}
