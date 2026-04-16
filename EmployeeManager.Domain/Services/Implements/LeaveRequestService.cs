using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Entity.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using EmployeeManager.Common.Constants;

namespace EmployeeManager.Domain.Services.Implements
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<LeaveRequest> _logger;

        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository, ILogger<LeaveRequest> logger)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
            _logger = logger;
        }
        public List<LeaveRequest> GetLeaveRequestsByEmployeeId(long employeeId)
        {
            try
            {
                Employee? employee = _employeeRepository.GetById(employeeId).Result;
                if (employee == null)
                {
                    _logger.LogWarning($"Employee with ID {employeeId} not found.");
                    throw new Exception($"Employee with ID {employeeId} not found.");
                }
                else
                {
                    List<LeaveRequest> leaveRequests = _leaveRequestRepository.GetLeaveRequestsByEmployeeId(employeeId);
                    return leaveRequests;
                }
            }
            catch (Exception ex) { 
                _logger.LogError(ex, $"Error occurred while getting leave requests for employee with ID: {employeeId}");
                throw new Exception($"Error occurred while getting leave requests for employee with ID: {employeeId}", ex);
            }
        }

        public void RegisterLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                _leaveRequestRepository.InsertAsync(leaveRequest).Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while registering leave request for employee with ID: {leaveRequest.EmployeeId}");
                throw new Exception($"Error occurred while registering leave request for employee with ID: {leaveRequest.EmployeeId}", ex);
            }
        }

        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            try
            {
                LeaveRequest? existingLeaveRequest = _leaveRequestRepository.GetById(leaveRequest.Id).Result;
                if (existingLeaveRequest == null)
                {
                    _logger.LogError($"Leave request with ID {leaveRequest.Id} not found.");
                    throw new Exception($"Leave request with ID {leaveRequest.Id} not found.");
                }
                else
                {
                    // Employee cannot update STATUS
                    if(existingLeaveRequest.Status != leaveRequest.Status)
                    {
                        _logger.LogError("Employee are not allowed to update status");
                        throw new Exception("Employee are not allowed to update status");
                    }
                    else
                    {
                        _leaveRequestRepository.UpdateAsync(existingLeaveRequest, leaveRequest).Wait();
                    }
                }
            }
            catch (Exception ex) { 
                _logger.LogError(ex, $"Error occurred while updating leave request with ID: {leaveRequest.Id}");
                throw new Exception($"Error occurred while updating leave request with ID: {leaveRequest.Id}", ex);
            }
        }

        public void ApproveLeaveRequest(long leaveRequestId, bool isApproved)
        {
            try
            {
                LeaveRequest? leaveRequest = _leaveRequestRepository.GetById(leaveRequestId).Result;
                if (leaveRequest == null)
                {
                    _logger.LogError($"Leave request with ID {leaveRequestId} not found.");
                    throw new Exception($"Leave request with ID {leaveRequestId} not found.");
                }
                else
                {
                    LeaveRequest updatedLeaveRequest = new LeaveRequest
                    {
                        Id = leaveRequest.Id,
                        EmployeeId = leaveRequest.EmployeeId,
                        StartDate = leaveRequest.StartDate,
                        EndDate = leaveRequest.EndDate,
                        Type = leaveRequest.Type,
                        Status = isApproved ? Constants.APPROVED : Constants.REJECTED
                    };
                    _leaveRequestRepository.UpdateAsync(leaveRequest, updatedLeaveRequest).Wait();
                }
            }
            catch (Exception ex) { 
                _logger.LogError(ex, $"Error occurred while approving leave request with ID: {leaveRequestId}");
                throw new Exception($"Error occurred while approving leave request with ID: {leaveRequestId}", ex);
            }
        }
        public void DeleteLeaveRequest(long leaveRequestId)
        {
            try
            {
                LeaveRequest? leaveRequest = _leaveRequestRepository.GetById(leaveRequestId).Result;
                if (leaveRequest == null)
                {
                    _logger.LogError($"Leave request with ID {leaveRequestId} not found.");
                    throw new Exception($"Leave request with ID {leaveRequestId} not found.");
                }
                else
                {
                    _leaveRequestRepository.DeleteAsync(leaveRequest).Wait();
                }
            }
            catch (Exception ex) { 
                _logger.LogError(ex, $"Error occurred while deleting leave request with ID: {leaveRequestId}");
                throw new Exception($"Error occurred while deleting leave request with ID: {leaveRequestId}", ex);
            }
        }

    }
}
