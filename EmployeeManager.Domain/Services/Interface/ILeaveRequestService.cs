using EmployeeManager.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Services.Interface
{
    public interface ILeaveRequestService
    {
        List<LeaveRequest> GetLeaveRequestsByEmployeeId(long employeeId);
        void RegisterLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void ApproveLeaveRequest(long leaveRequestId, bool isApproved);
        void DeleteLeaveRequest(long leaveRequestId);
    }
}
