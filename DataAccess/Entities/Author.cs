namespace Labb2_DbFirst_Template.Entities;

public partial class Author
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public virtual ICollection<Title> Isbns { get; set; } = new List<Title>();
}
