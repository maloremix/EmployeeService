using Employees.DAL.Models;

namespace Employees.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория для операций с сущностями сотрудников.
    /// </summary>
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        /// <summary>
        /// Возвращает список сотрудников, принадлежащих к указанной компании.
        /// </summary>
        /// <param name="companyId">Идентификатор компании.</param>
        /// <returns>Список сотрудников компании.</returns>
        Task<List<Employee>> SelectByCompany(int companyId);

        /// <summary>
        /// Возвращает список сотрудников, принадлежащих к указанному департаменту.
        /// </summary>
        /// <param name="departmentId">Идентификатор департамента.</param>
        /// <returns>Список сотрудников департамента.</returns>
        Task<List<Employee>> SelectByDepartment(int departmentId);
    }
}
