using AutoMapper;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Entity.Entities;
using EmployeeManager.WebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class PayrollController : Controller
    {
        private readonly IPayrollService _payrollService;
        private readonly ILogger<Payroll> _logger;
        private readonly IMapper _automapper;

        public PayrollController(IPayrollService payrollService, ILogger<Payroll> logger, IMapper automapper)
        {
            _payrollService = payrollService;
            _logger = logger;
            _automapper = automapper;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get([FromQuery] int employeeId)
        {
            try
            {
                _logger.LogInformation("Getting payrolls for employee with ID {EmployeeId}", employeeId);
                List<Payroll> payrolls = _payrollService.GetPayrollsByEmployeeId(employeeId);
                List<PayrollModel> payrollResponse = new List<PayrollModel>();

                foreach (Payroll payroll in payrolls) { 
                    PayrollModel payrollModel = _automapper.Map<PayrollModel>(payroll);
                    payrollResponse.Add(payrollModel);
                }

                return Ok(payrollResponse);
            }
            catch (Exception ex) { 
                _logger.LogError(ex, "Error getting payrolls for employee with ID {EmployeeId}", employeeId);
                return BadRequest(ex.StackTrace + "\n" + ex.Message);
            }
        }
    }
}
