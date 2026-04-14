using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManager.Entity.Entities;

namespace EmployeeManager.Domain.Services.Interface
{
    public interface IPayrollService
    {
        List<Payroll> GetPayrollsByEmployeeId(long employeeId);
    }
}
