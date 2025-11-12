using MyMvcApp.ViewModels;

namespace MyMvcApp.IService;

public interface IEmployeeService
{
    public Task<(List<EmployeeViewModel>, int, int)> GetAllEmployee(string search, int pageNumber);
    public Task CreateNewEmployee(EmployeeViewModel employee);
    public Task UpdateEmployee(EmployeeViewModel employee);
    public Task DeleteEmployee(int employeeId);
    public Task<EmployeeViewModel> GetEmployeeById(int employeeId);
    public Task UploadNewFile(IFormFile file);
    public Task<byte[]> DownloadFile(string fileName);
    public Task<List<string>> GetAllFiles();
}