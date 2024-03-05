namespace Common.Models;

public class StockModel
{
    public int StoreId { get; set; }
    public string Isbnid { get; set; } = null!;
    public int? NumberOfBooks { get; set; }
    public virtual TitleModel Isbn { get; set; } = null!;
    public virtual StoreModel Store { get; set; } = null!;
}