using EmployeeManager.Base.Implementation;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Entity.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Repositories.Implements
{
    public class PayrollRepository : BaseRepository<Payroll>, IPayrollRepository
    {
        private readonly EmployeeManagerContext _dbContext;
        private readonly ILogger<Payroll> _logger;
        public PayrollRepository(EmployeeManagerContext dbContext, ILogger<Payroll> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<Payroll> GetPayrollByEmployeeId(long employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero", nameof(employeeId));
            }
            List<Payroll> payrolls = _dbContext.Payrolls.Where(p => p.EmployeeId == employeeId).ToList();

            if(payrolls.Count == 0)
            {
                _logger.LogWarning("No payroll records found for employee with ID {EmployeeId}", employeeId);
            }

            return payrolls;
        }

    }
}
