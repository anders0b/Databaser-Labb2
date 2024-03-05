namespace Common.Models;

public class EmployeeModel
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public DateOnly EmployeeSince { get; set; }
    public string ContactEmail { get; set; }
    public int Phone { get; set; }
    public StoreModel Store { get; set; }

}