using MyMvcApp.IService;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Service;

public class EmployeeApiService : IEmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task CreateNewEmployee(EmployeeViewModel employee)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/employee", employee);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteEmployee(int employeeId)
    {
        var response = await _httpClient.DeleteAsync($"api/employee/delete/{employeeId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<(List<EmployeeViewModel>, int, int)> GetAllEmployee(string search, int pageNumber)
    {
        var response = await _httpClient.GetAsync($"api/employee?search={search}&page={pageNumber}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<EmployeeApiRepsonseViewModel<GetAllEmployeeViewModel>>();
        return (
            result?.Data?.Employees ?? new List<EmployeeViewModel>(),
            result?.Data?.TotalCount ?? 0,
            result?.Data?.PageSize ?? 0
        );
    }

    public async Task<EmployeeViewModel> GetEmployeeById(int employeeId)
    {
        var response = await _httpClient.GetAsync($"api/employee/user/{employeeId}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<EmployeeApiRepsonseViewModel<EmployeeViewModel>>();
        return result?.Data ?? new EmployeeViewModel();
    }

    public async Task UpdateEmployee(EmployeeViewModel employee)
    {
        var resposne = await _httpClient.PutAsJsonAsync($"api/employee/edit", employee);
        resposne.EnsureSuccessStatusCode();
    }

    public async Task UploadNewFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("File is required");
        }

        using var content = new MultipartFormDataContent();
        using var fileStream = file.OpenReadStream();
        using var streamContent = new StreamContent(fileStream);

        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

        content.Add(streamContent, "file", file.FileName);

        var response = await _httpClient.PostAsync("api/employee/upload", content);
        response.EnsureSuccessStatusCode();
    }
}