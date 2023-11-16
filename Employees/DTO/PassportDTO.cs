using System.ComponentModel.DataAnnotations;

namespace Employees.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) для представления информации о паспорте сотрудника.
    /// </summary>
    public class PassportDTO
    {
        /// <summary>
        /// Тип паспорта.
        /// </summary>
        [Required(ErrorMessage = "Тип паспорта обязателен для заполнения")]
        public string Type { get; set; }

        /// <summary>
        /// Номер паспорта.
        /// </summary>
        [Required(ErrorMessage = "Номер паспорта обязателен для заполнения")]
        [RegularExpression(@"^\d{4} \d{6}$", ErrorMessage = "Формат номера паспорта должен быть: 4 цифры, пробел, 6 цифр")]
        public string Number { get; set; }
    }
}
