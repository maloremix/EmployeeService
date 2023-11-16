namespace Employees.DAL.Models
{
    /// <summary>
    /// Представляет информацию о сотруднике в системе.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Уникальный идентификатор сотрудника.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Телефонный номер сотрудника.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Идентификатор компании, к которой принадлежит сотрудник.
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Паспортные данные сотрудника.
        /// </summary>
        public Passport Passport { get; set; }

        /// <summary>
        /// Департамент, к которому принадлежит сотрудник.
        /// </summary>
        public Department Department { get; set; }
    }
}
