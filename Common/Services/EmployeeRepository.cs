using Common.Models;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.Services;

public class EmployeeRepository
{
    private readonly AndersBooksContext _context;

    public EmployeeRepository()
    {
        _context = new AndersBooksContext();
    }

    public EmployeeRepository(AndersBooksContext context)
    {
        _context = context;
    }

    public IEnumerable<EmployeeModel> GetAllEmployees()
    {
        return _context.Employees.Include(s => s.Store).Select(employee =>  new EmployeeModel
        {
            Id = employee.Id,
            StoreId = (int)employee.StoreId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            JobTitle = employee.JobTitle,
            EmployeeSince = (DateOnly)employee.EmployeeSince,
            ContactEmail = employee.ContactEMail,
            Phone = (int)employee.Phone,
        }).ToList();
    }

}