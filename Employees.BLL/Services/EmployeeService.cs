using Employees.DAL.Interfaces;
using Employees.DAL.Models;

/// <summary>
/// Сервис для управления сотрудниками.
/// </summary>
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository employeeRepository;

    /// <summary>
    /// Инициализирует новый экземпляр класса EmployeeService.
    /// </summary>
    /// <param name="employeeRepository">Репозиторий сотрудников.</param>
    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
    }

    /// <summary>
    /// Получает список всех сотрудников.
    /// </summary>
    public async Task<List<Employee>> GetEmployees()
    {
        return await employeeRepository.Select();
    }

    /// <summary>
    /// Получает сотрудника по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника.</param>
    public async Task<Employee> GetEmployeeById(int id)
    {
        return await employeeRepository.Get(id);
    }

    /// <summary>
    /// Создает нового сотрудника.
    /// </summary>
    /// <param name="employee">Данные нового сотрудника.</param>
    public async Task<Employee> CreateEmployee(Employee employee)
    {
        return await employeeRepository.Create(employee);
    }

    /// <summary>
    /// Обновляет информацию о существующем сотруднике.
    /// </summary>
    /// <param name="employee">Информация для обновления сотрудника.</param>
    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        return await employeeRepository.Update(employee);
    }

    /// <summary>
    /// Удаляет сотрудника по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника.</param>
    public async Task<Employee> DeleteEmployee(int id)
    {
        return await employeeRepository.Delete(id);
    }

    /// <summary>
    /// Получает список сотрудников по идентификатору компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    public async Task<List<Employee>> GetEmployeesByCompany(int companyId)
    {
        return await employeeRepository.SelectByCompany(companyId);
    }

    /// <summary>
    /// Получает список сотрудников по идентификатору департамента.
    /// </summary>
    /// <param name="departmentId">Идентификатор департамента.</param>
    public async Task<List<Employee>> GetEmployeesByDepartment(int departmentId)
    {
        return await employeeRepository.SelectByDepartment(departmentId);
    }
}
