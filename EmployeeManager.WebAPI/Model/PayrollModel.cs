using EmployeeManager.Entity.Entities;

namespace EmployeeManager.WebAPI.Model
{
    public class PayrollModel
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public DateOnly PayDate { get; set; }

        public decimal BaseSalary { get; set; }

        public decimal? Bonus { get; set; }

        public decimal? Deductions { get; set; }

        public decimal? NetSalary { get; set; }
    }
}
