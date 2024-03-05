namespace Common.Models;

public class TitleModel
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public string Language { get; set; }
    public int Pages { get; set; }
    public double Price { get; set; }
    public DateOnly YearOfPublication { get; set; }
    public virtual ICollection<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();
    public virtual ICollection<StockModel> Stock { get; set; } = new List<StockModel>();
    public virtual ICollection<AuthorModel> Authors { get; set; } = new List<AuthorModel>();
}