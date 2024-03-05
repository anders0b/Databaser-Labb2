namespace Labb2_DbFirst_Template.Entities;

public partial class Title
{
    public string Isbn { get; set; } = null!;

    public string? Title1 { get; set; }

    public string? Language { get; set; }

    public int? Pages { get; set; }

    public double? Price { get; set; }

    public DateOnly? YearOfPublication { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
