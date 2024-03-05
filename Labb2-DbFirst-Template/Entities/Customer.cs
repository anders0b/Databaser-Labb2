namespace Labb2_DbFirst_Template.Entities;

public partial class Customer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EMail { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
