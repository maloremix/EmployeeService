namespace Employees.DAL.Models
{
    /// <summary>
    /// Представляет компанию в системе.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Уникальный идентификатор компании.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название компании.
        /// </summary>
        public string Name { get; set; }
    }
}
