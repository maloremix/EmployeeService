namespace Employees.DAL.Models
{
    /// <summary>
    /// Представляет департамент в системе.
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Уникальный идентификатор департамента.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название департамента.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефонный номер департамента.
        /// </summary>
        public string Phone { get; set; }
    }
}
