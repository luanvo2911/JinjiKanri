using System.ComponentModel.DataAnnotations;

namespace JinjiKanri.WebAPI.Model
{
    public class EmployeesModel
    {
        
        public string fullName { get; set; }
        public string department { get; set; }
        public string position { get; set; }
        public string salary { get; set; }
        public string status { get; set; }
    }
}
