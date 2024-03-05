namespace Labb2_DbFirst_Template.Entities;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public string BookId { get; set; } = null!;

    public int? Quantity { get; set; }

    public virtual Title Book { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
