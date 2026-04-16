using EmployeeManager.Base.Implementation;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Entity.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Repositories.Implements
{
    public class LeaveRequestRepository : BaseRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly EmployeeManagerContext _dbContext;
        private readonly ILogger<LeaveRequest> _logger;

        public LeaveRequestRepository(EmployeeManagerContext dbContext, ILogger<LeaveRequest> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public List<LeaveRequest> GetLeaveRequestsByEmployeeId(long employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Employee ID must be greater than zero", nameof(employeeId));
            }
            List<LeaveRequest> leaveRequests = _dbContext.LeaveRequests.Where(lr => lr.EmployeeId == employeeId).ToList();
            return leaveRequests;
        }
    }
}
