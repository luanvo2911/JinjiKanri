using EmployeeManager.Base.Interface;
using EmployeeManager.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Repositories.Interface
{
    public interface ILeaveRequestRepository : IBaseRepository<LeaveRequest>
    {
        List<LeaveRequest> GetLeaveRequestsByEmployeeId(long employeeId);
    }
}
