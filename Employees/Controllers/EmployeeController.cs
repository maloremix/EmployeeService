using Microsoft.AspNetCore.Mvc;
using Employees.DTO;
using Employees.DAL.Models;
using AutoMapper;

/// <summary>
/// Контроллер для управления сотрудниками.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    /// <summary>
    /// Сервис для управления сотрудниками.
    /// </summary>
    private readonly IEmployeeService employeeService;

    /// <summary>
    /// Объект AutoMapper для маппинга между объектами сущностей и DTO.
    /// </summary>
    private readonly IMapper mapper;

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="EmployeeController"/>.
    /// </summary>
    /// <param name="employeeService">Сервис сотрудников.</param>
    /// <param name="mapper">Экземпляр AutoMapper.</param>
    public EmployeeController(IEmployeeService employeeService, IMapper mapper)
    {
        this.employeeService = employeeService;
        this.mapper = mapper;
    }

    /// <summary>
    /// Получает список всех сотрудников.
    /// </summary>
    /// <returns>Список сотрудников.</returns>
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await employeeService.GetEmployees();

        var employeeDTOs = mapper.Map<List<EmployeeDTO>>(employees);

        return Ok(employeeDTOs);
    }

    /// <summary>
    /// Получает сотрудника по ID.
    /// </summary>
    /// <param name="id">ID сотрудника.</param>
    /// <returns>Сотрудник с указанным ID.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await employeeService.GetEmployeeById(id);

        if (employee == null)
            return NotFound();

        var employeeDTO = mapper.Map<EmployeeDTO>(employee);

        return Ok(employeeDTO);
    }

    /// <summary>
    /// Создает нового сотрудника.
    /// </summary>
    /// <param name="employeeDTO">DTO, представляющий нового сотрудника.</param>
    /// <returns>ID созданного сотрудника.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDTO)
    {
        if (employeeDTO == null)
            return BadRequest();

        var employee = mapper.Map<Employee>(employeeDTO);

        var createdEmployee = await employeeService.CreateEmployee(employee);

        var createdEmployeeDTO = mapper.Map<EmployeeDTO>(createdEmployee);

        return Ok(new { id = createdEmployeeDTO.Id });
    }

    /// <summary>
    /// Обновляет существующего сотрудника.
    /// </summary>
    /// <param name="id">ID сотрудника для обновления.</param>
    /// <param name="employeeEditDTO">DTO, представляющий обновленные данные сотрудника.</param>
    /// <returns>DTO обновленного сотрудника.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeEditDTO employeeEditDTO)
    {
        if (employeeEditDTO == null || id != employeeEditDTO.Id)
            return BadRequest();

        var existingEmployee = await employeeService.GetEmployeeById(id);

        if (existingEmployee == null)
            return NotFound();

        // Маппим свойства из EditDTO в существующий объект
        mapper.Map(employeeEditDTO, existingEmployee);

        var updatedEmployee = await employeeService.UpdateEmployee(existingEmployee);

        // Возвращаем DTO обновленного сотрудника
        var updatedEmployeeDTO = mapper.Map<EmployeeDTO>(updatedEmployee);

        return Ok(updatedEmployeeDTO);
    }

    /// <summary>
    /// Удаляет сотрудника по ID.
    /// </summary>
    /// <param name="id">ID сотрудника для удаления.</param>
    /// <returns>DTO удаленного сотрудника.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var deletedEmployee = await employeeService.DeleteEmployee(id);

        var deletedEmployeeDTO = mapper.Map<EmployeeDTO>(deletedEmployee);

        return Ok(deletedEmployeeDTO);
    }

    /// <summary>
    /// Получает список сотрудников в определенной компании.
    /// </summary>
    /// <param name="companyId">ID компании.</param>
    /// <returns>Список сотрудников в указанной компании.</returns>
    [HttpGet("company/{companyId}")]
    public async Task<IActionResult> GetEmployeesByCompany(int companyId)
    {
        var employees = await employeeService.GetEmployeesByCompany(companyId);

        if (employees == null || employees.Count == 0)
            return NotFound();

        var employeeDTOs = mapper.Map<List<EmployeeDTO>>(employees);

        return Ok(employeeDTOs);
    }

    /// <summary>
    /// Получает список сотрудников в определенном отделе.
    /// </summary>
    /// <param name="departmentId">ID отдела.</param>
    /// <returns>Список сотрудников в указанном отделе.</returns>
    [HttpGet("department/{departmentId}")]
    public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
    {
        var employees = await employeeService.GetEmployeesByDepartment(departmentId);

        if (employees == null || employees.Count == 0)
            return NotFound();

        var employeeDTOs = mapper.Map<List<EmployeeDTO>>(employees);

        return Ok(employeeDTOs);
    }
}
