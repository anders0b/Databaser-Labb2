namespace Common.Models;

public class OrderDetailModel
{
    public int OrderId { get; set; }

    public string BookId { get; set; } = null!;

    public int? Quantity { get; set; }

    public virtual TitleModel Book { get; set; } = null!;

    public virtual OrderModel Order { get; set; } = null!;
}