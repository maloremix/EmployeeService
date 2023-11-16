using System.ComponentModel.DataAnnotations;

namespace Employees.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) для представления информации о сотруднике.
    /// </summary>
    public class EmployeeDTO
    {
        /// <summary>
        /// Уникальный идентификатор сотрудника.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя сотрудника.
        /// </summary>
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        [StringLength(50, ErrorMessage = "Длина имени не должна превышать 50 символов")]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия сотрудника.
        /// </summary>
        [Required(ErrorMessage = "Фамилия обязательна для заполнения")]
        [StringLength(50, ErrorMessage = "Длина фамилии не должна превышать 50 символов")]
        public string Surname { get; set; }

        /// <summary>
        /// Номер телефона сотрудника.
        /// </summary>
        [Required(ErrorMessage = "Телефон обязателен для заполнения")]
        [RegularExpression(@"\+7\d{3}\d{3}\d{2}\d{2}", ErrorMessage = "Формат телефонного номера должен быть +7xxxxxxxxxx")]
        public string Phone { get; set; }

        /// <summary>
        /// Идентификатор компании, к которой принадлежит сотрудник.
        /// </summary>
        [Range(1, 5, ErrorMessage = "CompanyId должен быть больше 0 и меньше 5")]
        public int CompanyId { get; set; }

        /// <summary>
        /// Паспортные данные сотрудника.
        /// </summary>
        public PassportDTO Passport { get; set; }

        /// <summary>
        /// Информация о департаменте, к которому принадлежит сотрудник.
        /// </summary>
        public DepartmentDTO Department { get; set; }
    }
}
