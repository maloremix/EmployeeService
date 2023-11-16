using Employees.DAL.Models;

/// <summary>
/// Интерфейс сервиса для управления сотрудниками.
/// </summary>
public interface IEmployeeService
{
    /// <summary>
    /// Получает список всех сотрудников.
    /// </summary>
    /// <returns>Список сотрудников.</returns>
    Task<List<Employee>> GetEmployees();

    /// <summary>
    /// Получает сотрудника по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника.</param>
    /// <returns>Сотрудник.</returns>
    Task<Employee> GetEmployeeById(int id);

    /// <summary>
    /// Создает нового сотрудника.
    /// </summary>
    /// <param name="employee">Данные нового сотрудника.</param>
    /// <returns>Созданный сотрудник.</returns>
    Task<Employee> CreateEmployee(Employee employee);

    /// <summary>
    /// Обновляет информацию о существующем сотруднике.
    /// </summary>
    /// <param name="employee">Информация для обновления сотрудника.</param>
    /// <returns>Обновленный сотрудник.</returns>
    Task<Employee> UpdateEmployee(Employee employee);

    /// <summary>
    /// Удаляет сотрудника по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сотрудника.</param>
    /// <returns>Удаленный сотрудник.</returns>
    Task<Employee> DeleteEmployee(int id);

    /// <summary>
    /// Получает список сотрудников по идентификатору компании.
    /// </summary>
    /// <param name="companyId">Идентификатор компании.</param>
    /// <returns>Список сотрудников компании.</returns>
    Task<List<Employee>> GetEmployeesByCompany(int companyId);

    /// <summary>
    /// Получает список сотрудников по идентификатору департамента.
    /// </summary>
    /// <param name="departmentId">Идентификатор департамента.</param>
    /// <returns>Список сотрудников департамента.</returns>
    Task<List<Employee>> GetEmployeesByDepartment(int departmentId);
}
