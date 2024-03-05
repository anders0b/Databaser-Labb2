namespace Common.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public virtual ICollection<TitleModel> Isbns { get; set; } = new List<TitleModel>();
    }
}
