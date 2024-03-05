using Common.Models;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class StoreRepository
{
    private readonly AndersBooksContext _context;

    public StoreRepository()
    {
        _context = new AndersBooksContext();
    }

    public StoreRepository(AndersBooksContext context)
    {
        _context = context;
    }
    public static event Action StoreListChanged;

    public void AddStore(StoreModel store)
    {
        var newStore = new Store();
        newStore.Name = store.Name;
        newStore.StreetAddress = store.StreetAddress;
        newStore.PostalCode = store.PostalCode;
        newStore.City = store.City;
        foreach (var employee in store.Employees)
        {
            newStore.Employees.Add(_context.Employees.FirstOrDefault(e => e.Id == employee.Id));
        }
        _context.Add(newStore);
        _context.SaveChanges();
        StoreListChanged.Invoke();
    }

    public void RemoveStore(StoreModel store)
    {
        _context.Stores.Remove(_context.Stores.FirstOrDefault(s => s.Id == store.Id));
        _context.SaveChanges();
        StoreListChanged.Invoke();
    }

    public IEnumerable<StoreModel> GetAllStores()
    {
        return _context.Stores.Include(e => e.Employees).Include(s => s.Stocks).Select
        (
            store => new StoreModel
            {
                Id = store.Id,
                Name = store.Name,
                StreetAddress = store.StreetAddress,
                PostalCode = (int)store.PostalCode,
                City = store.City,
                Employees = store.Employees.Select(employee => new EmployeeModel
                {
                    StoreId = (int)employee.StoreId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    JobTitle = employee.JobTitle,
                    EmployeeSince = (DateOnly)employee.EmployeeSince,
                    ContactEmail = employee.ContactEMail,
                    Phone = (int)employee.Phone
                }).ToList(),
                Stocks = store.Stocks.Select(stock => new StockModel
                {
                    StoreId = stock.StoreId,
                    Isbnid = stock.Isbnid,
                    NumberOfBooks = stock.NumberOfBooks,
                }).ToList()
            }).ToList();
    }

    public void UpdateStore(StoreModel store)
    {
        var storeUpdate = _context.Stores.Include(e => e.Employees).FirstOrDefault(s => s.Id == store.Id);

        storeUpdate.Id = store.Id;
        storeUpdate.Name = store.Name;
        storeUpdate.StreetAddress = store.StreetAddress;
        storeUpdate.PostalCode = store.PostalCode;
        storeUpdate.City = store.City;
        _context.SaveChanges();
        StoreListChanged.Invoke();
    }

}