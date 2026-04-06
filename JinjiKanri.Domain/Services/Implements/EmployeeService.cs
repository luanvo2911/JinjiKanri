using JinjiKanri.Domain.Repositories.Interface;
using JinjiKanri.Domain.Services.Interface;
using JinjiKanri.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JinjiKanri.Domain.Services.Implements
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee?> GetEmployees()
        {
            return _employeeRepository.GetAll().Result.ToList();
        }
    }
}
