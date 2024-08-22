using MANTENIMIENTO_B3.Data;
using MANTENIMIENTO_B3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MANTENIMIENTO_B3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly AplicationDBContext _context;

        public EmployeeController(
           ILogger<EmployeeController> logger,
           AplicationDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.TypeEmployee).ToList();
            ViewBag.Employees = employees;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var typeEmployees = _context.TypeEmployees.ToList();
            ViewBag.TypeEmployees = typeEmployees;
            return View();
        }

        [HttpPost]
        public IActionResult Save(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.FechaCreacion = DateTime.Now;
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeEmployees = _context.TypeEmployees.ToList();
            return View("Create", employee);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Include(e => e.TypeEmployee).FirstOrDefault(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            var typeEmployees = _context.TypeEmployees.ToList();
            ViewBag.TypeEmployees = typeEmployees;
            return View(employee);
        }

        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);

                if (existingEmployee != null)
                {
                    existingEmployee.Nombre = employee.Nombre;
                    existingEmployee.Apellido = employee.Apellido;
                    existingEmployee.Dui = employee.Dui;
                    existingEmployee.NumeroTelefonico = employee.NumeroTelefonico;
                    existingEmployee.TypeEmployeeId = employee.TypeEmployeeId;
                    existingEmployee.FechaModificacion = DateTime.Now;
                    existingEmployee.IsActive = employee.IsActive;

                    _context.Employees.Update(existingEmployee);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.TypeEmployees = _context.TypeEmployees.ToList();
            return View("Edit", employee);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
