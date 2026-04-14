using EmployeeManager.Domain.Repositories.Interface;
using EmployeeManager.Domain.Services.Interface;
using EmployeeManager.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManager.Domain.Services.Implements
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
            try
            {
                return _employeeRepository.GetAll().Result.ToList();
            }
            catch (Exception ex) { 
                throw new Exception("An error occurred while retrieving employees: " + ex.Message, ex);
            }
        }

        public Employee? GetEmployee(long id)
        {
            try
            {
                return _employeeRepository.GetById(id).Result;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving employee with ID {id}: " + ex.Message, ex);
            }
        }

        public void InsertEmployee(Employee employee)
        {
            try
            {
                _employeeRepository.InsertAsync(employee).Wait();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while inserting employee with ID {employee.Id}: " + ex.Message, ex);
            }
        }

        public void UpdateEmployee(Employee employee, long id)
        {
            try
            {
                Employee? existingEmployee = _employeeRepository.GetById(id).Result;
                if (existingEmployee == null)
                {
                    throw new Exception("Employee that need to be updated does not exist");
                }
                else
                {
                    _employeeRepository.UpdateAsync(existingEmployee, employee).Wait();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating employee with ID {id}: " + ex.Message, ex);
            }
        }

        public void DeleteEmployee(long id)
        {
            try
            {
                Employee? deletedEmployee = _employeeRepository.GetById(id).Result;
                if (deletedEmployee == null)
                {
                    throw new Exception("Employee that need to be updated does not exist");
                }
                else
                {
                    _employeeRepository.DeleteAsync(deletedEmployee).Wait();
                }
            }
            catch (Exception ex) { 
                throw new Exception($"An error occurred while deleting employee with ID {id}: " + ex.Message, ex);
            }
        }
    }
}
