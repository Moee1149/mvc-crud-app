namespace MyMvcApp.ViewModels;

public class EmployeeApiRepsonseViewModel<T>
{
    public T Data { get; set; } = default!;
    public string Message { get; set; } = "";
}
public class GetAllEmployeeViewModel
{
    public List<EmployeeViewModel> Employees { get; set; } = new();
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
}


