using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Entity.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Services.Implements
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<Payroll> _logger;

        public PayrollService(IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository, ILogger<Payroll> logger) { 
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public List<Payroll> GetPayrollsByEmployeeId(long employeeId)
        {
            try
            {
                Employee? employee = _employeeRepository.GetById(employeeId).Result;
                if (employee == null)
                {
                    throw new Exception($"Employee with ID {employeeId} not found.");
                }
                else
                {
                    List<Payroll> payrolls = _payrollRepository.GetPayrollByEmployeeId(employeeId);
                    return payrolls;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting payrolls for employee with ID: {employeeId}");
                throw;
            }
        }
    }
}
