using JinjiKanri.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JinjiKanri.Domain.Services.Interface
{
    public interface IEmployeeService
    {
        public List<Employee?> GetEmployees();
    }
}
