namespace MyMvcApp.ViewModels;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string Department { get; set; } = "";
    public DateTime HireDate { get; set; }
}