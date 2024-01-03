using EmpData.Dal;
using EmpData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpData.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> TableAsync()
        {
            List<Employee> emp = await Api.GetAsync();
            return View(emp);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Delete(int id)
        {
            await Api.DeleteAsync(id);
            List<Employee> emp = await Api.GetAsync();
            return RedirectToAction("Table", emp);
        }
        public async Task<IActionResult> AddEdit(int? id = null)
        {
            if (id != null) return View(await Api.GetAsync(id));
            return View();
        }
        public async Task<IActionResult> Save(Employee emp, int? EmpID = null)
        {
            if (TryValidateModel(emp))
            {
                await Api.AddUpdateAsync(EmpID, emp);
                return RedirectToAction("Table");
            }
            return RedirectToAction("AddEdit");
        }
    }
}
