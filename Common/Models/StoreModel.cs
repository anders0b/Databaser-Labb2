namespace Common.Models;

public class StoreModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string StreetAddress { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public List<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();
    public List<StockModel> Stocks { get; set; } = new List<StockModel>();
}