using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        public IEmployeesData employees { get; set; } // Свойство для хранения сотрудников в контроллере через сервис
        public EmployeesController(IEmployeesData EmployeesData)
        {
            this.employees = EmployeesData; // Получаем данные по сотрудникам из класса TestData
        }

        public IActionResult Index()
        {
            return View(employees.GetAll());
        }

        public IActionResult Details(int Id)
        {
            var employee = employees.GetById(Id);

            if (employee is null)
                return NotFound();

            return View(employee);
        }

        public IActionResult Create() => View("Edit", new EmployeeViewModel());
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());

            var employee = employees.GetById((int)id);
            if (employee is null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };
            
            return View(model); // Отправка модели на обработку
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.Name,
                Patronymic = model.Patronymic,
                Age = model.Age,
            };

            if(model.Id == 0)
                employees.Add(employee);
            else if (!employees.Edit(employee))
                return NotFound();
                
            // Обработка модели
            return RedirectToAction("Index");
        }
           
        public IActionResult Delete(int id)
        {
            if(id < 0)
                return BadRequest();
            
            var employee = employees.GetById(id);
            if (employee is null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                LastName = employee.LastName,
                Name = employee.FirstName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };

            return View(model); // Отправка модели на обработку
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var employee = employees.GetById(id);
            if (employee is null)
                return NotFound();

            employees.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
