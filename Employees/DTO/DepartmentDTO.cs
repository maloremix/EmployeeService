using System.ComponentModel.DataAnnotations;

namespace Employees.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) для представления информации о департаменте.
    /// </summary>
    public class DepartmentDTO
    {
        /// <summary>
        /// Название департамента.
        /// </summary>
        [Required(ErrorMessage = "Название департамента обязательно для заполнения")]
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона департамента.
        /// </summary>
        [Required(ErrorMessage = "Телефон департамента обязателен для заполнения")]
        [RegularExpression(@"\+7\d{3}\d{3}\d{2}\d{2}", ErrorMessage = "Формат телефонного номера должен быть +7xxxxxxxxxx")]
        public string Phone { get; set; }
    }
}
