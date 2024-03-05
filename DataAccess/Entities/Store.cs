namespace Labb2_DbFirst_Template.Entities;

public partial class Store
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? StreetAddress { get; set; }

    public int? PostalCode { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
