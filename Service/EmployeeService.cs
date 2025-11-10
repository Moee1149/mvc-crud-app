using Microsoft.EntityFrameworkCore;
using MyMvcApp.Data;
using MyMvcApp.IService;
using MyMvcApp.Models;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Service;

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;
    public EmployeeService(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateNewEmployee(EmployeeViewModel employeeViewModel)
    {
        Employee employee = MapViewToModel(employeeViewModel);
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
    }

    private Employee MapViewToModel(EmployeeViewModel vm)
    {
        return new Employee
        {
            Id = vm.Id,
            Name = vm.Name,
            Email = vm.Email,
            Department = vm.Department,
            HireDate = vm.HireDate
        };
    }

    public async Task DeleteEmployee(int employeeId)
    {
        Employee? employee = await _context.Employees.FindAsync(employeeId);
        if (employee == null)
        {
            throw new Exception("user not found");
        }
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();

    }

    public async Task<List<EmployeeViewModel>> GetAllEmployee(string search = "")
    {
        return await _context.Employees.Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            Name = e.Name,
            Email = e.Email,
            Department = e.Department,
            HireDate = e.HireDate
        }).ToListAsync();
    }

    public async Task UpdateEmployee(EmployeeViewModel employeeViewData)
    {
        Employee employee = MapViewToModel(employeeViewData);
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<EmployeeViewModel> GetEmployeeById(int employeeId)
    {
        Employee employee = _context.Employees.Find(employeeId) ?? new Employee { };
        if (employee == null)
        {
            throw new Exception("employee not found");
        }
        return new EmployeeViewModel
        {
            Id = employee.Id,
            Department = employee.Department,
            Email = employee.Email,
            HireDate = employee.HireDate,
            Name = employee.Name
        };
    }
}