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
    public async Task<IActionResult> GetAllFile()
    {
        var files = await _employeeService.GetAllFiles();
        Console.WriteLine("files" + files);
        return View("FileView", files);
    }

    [HttpPost]
    [Route("Employee/File/Upload")]
    public async Task<IActionResult> UploadNewFile(IFormFile file)
    {
        await _employeeService.UploadNewFile(file);
        Console.WriteLine("okay file uploaded");
        return RedirectToAction(nameof(GetAllFile));
    }

    [HttpGet]
    [Route("Employee/File/Download")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        Console.WriteLine("file name " + fileName);
        var fileBytes = await _employeeService.DownloadFile(fileName);
        return File(fileBytes, "application/octet-stream", fileName);
    }

}
