using MyMvcApp.ViewModels;

namespace MyMvcApp.IService;

public interface IEmployeeService
{
    public Task<(List<EmployeeViewModel>, int)> GetAllEmployee(string search, int pageSize, int pageNumber);
    public Task CreateNewEmployee(EmployeeViewModel employee);
    public Task UpdateEmployee(EmployeeViewModel employee);
    public Task DeleteEmployee(int employeeId);
    public Task<EmployeeViewModel> GetEmployeeById(int employeeId);
}