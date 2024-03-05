using Common.Models;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class StockRepository
{
    private readonly AndersBooksContext _context;

    public StockRepository()
    {
        _context = new AndersBooksContext();
    }

    public StockRepository(AndersBooksContext context)
    {
        _context = context;
    }
    public static event Action StockListChanged;

    public void AddStock(StockModel stock)
    {
        var stockToEdit = _context.Stocks.FirstOrDefault(s => s.StoreId == stock.StoreId && s.Isbnid.Equals(stock.Isbnid));
        if (stockToEdit != null)
        {
            stockToEdit.NumberOfBooks = stock.NumberOfBooks;
            _context.Stocks.Update(stockToEdit);
            _context.SaveChanges();
            StockListChanged?.Invoke();
        }
        else
        {
            _context.Add
            (
                new Stock
                {
                    StoreId = stock.StoreId,
                    Isbnid = stock.Isbnid,
                    NumberOfBooks = stock.NumberOfBooks
                });

            _context.SaveChanges();
            StockListChanged?.Invoke();
        }
    }

    public IEnumerable<StockModel> GetAllStock()
    {
        return _context.Stocks.Include(s => s.Store).Include(t => t.Isbn).Select
        (
            stock => new StockModel
            {
                StoreId = stock.StoreId,
                Isbnid = stock.Isbnid,
                NumberOfBooks = stock.NumberOfBooks
            }
        ).ToList();
    }

    public void DeleteStock(StockModel stock)
    {
        var stockToDelete = _context.Stocks.FirstOrDefault(s => s.StoreId == stock.StoreId && s.Isbnid.Equals(stock.Isbnid));
        _context.Stocks.Remove(stockToDelete);
        _context.SaveChanges();
        StockListChanged.Invoke();
    }
}