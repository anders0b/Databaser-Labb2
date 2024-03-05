namespace Common.Models;

public class OrderModel
{
    public int CustomerId { get; set; }
    public DateOnly OrderDate { get; set; }
    public string DeliveryAddress { get; set; }
    public int DeliveryPostalCode { get; set; }
    public string DeliveryCity { get; set; }
    public virtual CustomerModel Customer { get; set; }
    public List<OrderDetailModel> OrderDetails { get; set; }
}