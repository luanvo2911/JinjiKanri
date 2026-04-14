using EmployeeManager.Entity.Entities;

namespace EmployeeManager.WebAPI.Model
{
    public class LeaveRequestModel
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Type { get; set; }

        public string? Status { get; set; }
    }
}
