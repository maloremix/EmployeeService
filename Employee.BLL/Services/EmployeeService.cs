using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Employees.DAL.Interfaces;
using Employees.DAL.

namespace Employee.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeRepository.Select();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.Get(id);
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            // Возможна добавление логики перед созданием сотрудника
            return await _employeeRepository.Create(employee);
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            // Возможна добавление логики перед обновлением сотрудника
            return await _employeeRepository.Update(employee);
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            // Возможна добавление логики перед удалением сотрудника
            return await _employeeRepository.Delete(id);
        }
    }

}
