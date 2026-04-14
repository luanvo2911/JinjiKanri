using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.WebAPI.Model;
using EmployeeManager.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Diagnostics;

namespace EmployeeManager.WebAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILogger<LeaveRequest> _logger;
        private readonly IMapper _automapper;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService, ILogger<LeaveRequest> logger, IMapper automapper)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
            _automapper = automapper;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] long employeeId)
        {
            try
            {
                _logger.LogInformation($"Getting leave requests for employee with ID: {employeeId}");

                List<LeaveRequest> leaveRequests = _leaveRequestService.GetLeaveRequestsByEmployeeId(employeeId);

                List<LeaveRequestModel> leaveRequestsResponse = new List<LeaveRequestModel>();

                foreach (var leaveRequest in leaveRequests)
                {
                    LeaveRequestModel leaveRequestModel = _automapper.Map<LeaveRequestModel>(leaveRequest);
                    leaveRequestsResponse.Add(leaveRequestModel);
                }

                return Ok(leaveRequestsResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting leave requests for employee with ID: {employeeId}");
                return BadRequest(ex.StackTrace + "\n" + ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Register([FromBody] LeaveRequestModel leaveRequest)
        {
            try
            {
                _logger.LogInformation($"Registering leave request for employee with ID: {leaveRequest.EmployeeId}");
                _leaveRequestService.RegisterLeaveRequest(_automapper.Map<LeaveRequest>(leaveRequest));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while registering leave request for employee with ID: {leaveRequest.EmployeeId}");
                return BadRequest(ex.StackTrace + "\n" + ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [HttpPut]
        public IActionResult Update([FromBody] LeaveRequestModel leaveRequest)
        {
            try
            {
                _logger.LogInformation($"Updating leave request for employee with ID: {leaveRequest.EmployeeId}");
                _leaveRequestService.UpdateLeaveRequest(_automapper.Map<LeaveRequest>(leaveRequest));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating leave request for employee with ID: {leaveRequest.EmployeeId}");
                return BadRequest(ex.StackTrace + "\n" + ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN, HR")]
        [HttpPut("Approve")]
        public IActionResult Approve([FromQuery] long leaveRequestId, [FromQuery] bool isApproved)
        {
            try
            {
                _logger.LogInformation($"Approving leave request for employee with ID: {leaveRequestId}");
                _leaveRequestService.ApproveLeaveRequest(leaveRequestId, isApproved);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while updating leave request for employee with ID: {leaveRequestId}");
                return BadRequest(ex.StackTrace + "\n" + ex.Message);
            }
        }

        [Authorize(Roles = "ADMIN, EMPLOYEE")]
        [HttpDelete]
        public IActionResult Delete([FromQuery] long leaveRequestId)
        {
            try
            {
                _logger.LogInformation($"Deleting leave request");
                _leaveRequestService.DeleteLeaveRequest(leaveRequestId);
                return Ok();
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.StackTrace + "\n" + ex.Message);
                }
            }
        }
    }
}
