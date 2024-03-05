using Common.Models;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class TitleRepository
{
    private readonly AndersBooksContext _context;

    public TitleRepository()
    {
        _context = new AndersBooksContext();
    }

    public TitleRepository(AndersBooksContext context)
    {
        _context = context;
    }
    public static event Action TitleListChanged;

    public void AddBookTitle(TitleModel title)
    {
        var titleToAdd = new Title();
        titleToAdd.Isbn = title.Isbn;
        titleToAdd.Title1 = title.Title;
        titleToAdd.Language = title.Language;
        titleToAdd.Pages = title.Pages;
        titleToAdd.Price = title.Price;
        titleToAdd.YearOfPublication = title.YearOfPublication;
        foreach (var author in title.Authors)
        {
            titleToAdd.Authors.Add(_context.Authors.FirstOrDefault(a => a.Id == author.Id));
        }
        _context.Add(titleToAdd);
        _context.SaveChanges();
        TitleListChanged?.Invoke();
    }

    public void RemoveTitle(TitleModel title)
    {
        var author = _context.Titles.Include(a => a.Authors).FirstOrDefault(t => t.Isbn == title.Isbn);
        author.Authors.Clear();
        var stock = _context.Titles.Include(a => a.Stocks).FirstOrDefault(t => t.Isbn == title.Isbn);
        stock.Stocks.Clear();
        _context.SaveChanges();
        _context.Titles.Remove(_context.Titles.Include(a => a.Authors).FirstOrDefault(t => t.Isbn == title.Isbn));
        _context.SaveChanges();
        TitleListChanged?.Invoke();
    }

    public IEnumerable<TitleModel> GetAllBooks()
    {
        return _context.Titles.Include(a => a.Authors).Select
        (
            title => new TitleModel
            {
                Isbn = title.Isbn,
                Title = title.Title1,
                Language = title.Language,
                Pages = (int)title.Pages,
                Price = (double)title.Price,
                YearOfPublication = (DateOnly)title.YearOfPublication,
                Stock = title.Stocks.Select(stock => new StockModel
                    {
                        StoreId = stock.StoreId,
                        Isbnid = stock.Isbnid,
                        NumberOfBooks = stock.NumberOfBooks,
                    }).ToList(),
                Authors = title.Authors.Select(author => new AuthorModel
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    DateOfBirth = (DateOnly)author.DateOfBirth,
                }).ToList()
            }
        ).ToList();
    }


    public void UpdateTitle(TitleModel title)
    {
        var titleUpdate = _context.Titles.Include(a => a.Authors).FirstOrDefault(t => t.Title1 == title.Title);

        titleUpdate.Isbn = title.Isbn;
        titleUpdate.Title1 = title.Title;
        titleUpdate.Language = title.Language;
        titleUpdate.Pages = title.Pages;
        titleUpdate.Price = title.Price;
        titleUpdate.YearOfPublication = title.YearOfPublication;
        titleUpdate.Authors.Clear();
        _context.SaveChanges();
        foreach (var author in title.Authors)
        {
            titleUpdate.Authors.Add(_context.Authors.FirstOrDefault(a => a.Id == author.Id));
        }

        _context.SaveChanges();
        TitleListChanged.Invoke();
    }

}