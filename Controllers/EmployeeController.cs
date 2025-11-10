using Microsoft.AspNetCore.Mvc;
using MyMvcApp.IService;
using MyMvcApp.Models;
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
    public async Task<ActionResult> Index(string search)
    {
        List<EmployeeViewModel> employee = await _employeeService.GetAllEmployee(search);
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
}
