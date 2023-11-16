namespace Employees.DAL.Models
{
    /// <summary>
    /// Представляет паспортные данные сотрудника.
    /// </summary>
    public class Passport
    {
        /// <summary>
        /// Уникальный идентификатор паспорта.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Тип паспорта.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Номер паспорта.
        /// </summary>
        public string Number { get; set; }
    }
}
