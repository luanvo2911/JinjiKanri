using JinjiKanri.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JinjiKanri.Domain.Services.Interface
{
    public interface IEmployeeService
    {
        List<Employee?> GetEmployees();

        Employee? GetEmployee(long id);

        void InsertEmployee(Employee employee);

        void UpdateEmployee(Employee employee, long id);

        void DeleteEmployee(long id);

        


    }
}
