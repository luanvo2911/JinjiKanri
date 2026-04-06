using AutoMapper;
using JinjiKanri.Domain.Services.Interface;
using JinjiKanri.WebAPI.Model;
using JinjiKanri.Entity.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JinjiKanri.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _automapper;

        public EmployeesController(IEmployeeService employeeService, IMapper automapper) {
            _employeeService = employeeService;
            _automapper = automapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            EmployeesModel[] employees = _automapper.Map<EmployeesModel[]>(_employeeService.GetEmployees());

            return Ok(employees);
        }
    }
}
