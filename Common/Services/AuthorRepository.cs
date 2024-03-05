using Common.Models;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class AuthorRepository
{
    private readonly AndersBooksContext _context;

    public AuthorRepository()
    {
        _context = new AndersBooksContext();
    }

    public AuthorRepository(AndersBooksContext context)
    {
        _context = context;
    }
    public static event Action AuthorListChanged;

    public void AddAuthor(AuthorModel author)
    {
        var newAuthor = new Author();
        newAuthor.FirstName = author.FirstName;
        newAuthor.LastName = author.LastName;
        newAuthor.DateOfBirth = author.DateOfBirth;
        foreach (var isbn in author.Isbns)
        {
            newAuthor.Isbns.Add(_context.Titles.FirstOrDefault(t => t.Isbn == isbn.Isbn));
        }
        _context.Add(newAuthor);
        _context.SaveChanges();
        AuthorListChanged.Invoke();
    }

    public void RemoveAuthor(AuthorModel author)
    {
        var title = _context.Authors.Include(t => t.Isbns).FirstOrDefault(a => a.Id == author.Id);
        title.Isbns.Clear();
        _context.Authors.Remove(_context.Authors.FirstOrDefault(a => a.Id == author.Id));
        _context.SaveChanges();
        AuthorListChanged.Invoke();
    }

    public IEnumerable<AuthorModel> GetAllAuthors()
    {
        return _context.Authors.Include(t => t.Isbns).Select
        (
            author => new AuthorModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = (DateOnly)author.DateOfBirth,
                Isbns = author.Isbns.Select(title => new TitleModel
                {
                    Isbn = title.Isbn,
                    Title = title.Title1,
                    Language = title.Language,
                    Pages = (int)title.Pages,
                    Price = (double)title.Price,
                    YearOfPublication = (DateOnly)title.YearOfPublication,
                    OrderDetails = new List<OrderDetailModel>(),
                    Stock = new List<StockModel>(),
                    Authors = new List<AuthorModel>()
                }).ToList()
            }
        ).ToList();

    }

    public void UpdateAuthor(AuthorModel author)
    {
        var authorUpdate = _context.Authors.Include(t => t.Isbns).FirstOrDefault(a => a.Id == author.Id);

        authorUpdate.Id = author.Id;
        authorUpdate.FirstName = author.FirstName;
        authorUpdate.LastName = author.LastName;
        authorUpdate.DateOfBirth = author.DateOfBirth;
        authorUpdate.Isbns.Clear();
        _context.SaveChanges();
        foreach (var isbn in author.Isbns)
        {
            authorUpdate.Isbns.Add(_context.Titles.FirstOrDefault(t => t.Isbn == isbn.Isbn));
        }

        _context.SaveChanges();
        AuthorListChanged.Invoke();
    }
}