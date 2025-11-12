using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.IService;
using MyMvcApp.ViewModels;

namespace MyMvcApp.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService empolyeeService)
    {
        _employeeService = empolyeeService;
    }
    // GET: EmployeeController
    [HttpGet]
    public async Task<ActionResult> Index(string search, int page = 1)
    {
        var (employee, totalCount, pageSize) = await _employeeService.GetAllEmployee(search, page);
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalCount = totalCount;
        ViewBag.SearchTerm = search;
        return View(employee);
    }

    [HttpGet]
    public ActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
    {
        if (ModelState.IsValid)
        {
            await _employeeService.CreateNewEmployee(employeeViewModel);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [HttpGet]
    public async Task<ActionResult> Edit(int id)
    {
        EmployeeViewModel employeeViewModel = await _employeeService.GetEmployeeById(id);
        return View("Update", employeeViewModel);
    }


    [HttpPost]
    public async Task<ActionResult> Edit(EmployeeViewModel employeeViewModel)
    {
        await _employeeService.UpdateEmployee(employeeViewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(int id)
    {
        await _employeeService.DeleteEmployee(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("Employee/File")]
    public IActionResult GetAllFile()
    {
        return View("FileView");
    }

    [HttpPost]
    [Route("Employee/File/Upload")]
    public async Task<IActionResult> UploadNewFile(IFormFile file)
    {
        await _employeeService.UploadNewFile(file);
        Console.WriteLine("okay file uploaded");
        return View("FileView");
    }
}
