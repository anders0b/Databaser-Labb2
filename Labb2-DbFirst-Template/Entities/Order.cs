namespace Labb2_DbFirst_Template.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int? CustomerId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public string? DeliveryAddress { get; set; }

    public int? DeliveryPostalCode { get; set; }

    public string? DeliveryCity { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
