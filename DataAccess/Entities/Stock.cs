namespace Labb2_DbFirst_Template.Entities;

public partial class Stock
{
    public int StoreId { get; set; }

    public string Isbnid { get; set; } = null!;

    public int? NumberOfBooks { get; set; }

    public virtual Title Isbn { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
