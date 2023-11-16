using Dapper;
using System.Data;
using Employees.DAL.Interfaces;
using Employees.DAL.Models;
using Npgsql;

namespace Employees.DAL.Repositories
{
    /// <summary>
    /// Репозиторий для выполнения операций с данными о сотрудниках.
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string connectionString = null;

        /// <summary>
        /// Инициализирует новый экземпляр класса EmployeeRepository с заданной строкой подключения к базе данных.
        /// </summary>
        /// <param name="conn">Строка подключения к базе данных.</param>
        public EmployeeRepository(string conn)
        {
            connectionString = conn;
        }

        /// <summary>
        /// Получает список всех сотрудников.
        /// </summary>
        /// <returns>Список сотрудников.</returns>
        public async Task<List<Employee>> Select()
        {
            return await ExecuteSelectQuery(@"
                SELECT 
                    e.*, 
                    d.*,
                    p.*
                FROM Employees e
                LEFT JOIN Departments d ON e.DepartmentId = d.Id
                LEFT JOIN Passports p ON e.PassportId = p.Id;");
        }

        /// <summary>
        /// Получает список сотрудников по идентификатору компании.
        /// </summary>
        /// <param name="companyId">Идентификатор компании.</param>
        /// <returns>Список сотрудников компании.</returns>
        public async Task<List<Employee>> SelectByCompany(int companyId)
        {
            return await ExecuteSelectQuery(@"
                SELECT 
                    e.*, 
                    d.*,
                    p.*
                FROM Employees e
                LEFT JOIN Departments d ON e.DepartmentId = d.Id
                LEFT JOIN Passports p ON e.PassportId = p.Id
                WHERE e.CompanyId = @CompanyId;",
            new { CompanyId = companyId });
        }

        /// <summary>
        /// Получает список сотрудников по идентификатору департамента.
        /// </summary>
        /// <param name="departmentId">Идентификатор департамента.</param>
        /// <returns>Список сотрудников департамента.</returns>
        public async Task<List<Employee>> SelectByDepartment(int departmentId)
        {
            return await ExecuteSelectQuery(@"
        SELECT 
            e.*, 
            d.*,
            p.*
        FROM Employees e
        LEFT JOIN Departments d ON e.DepartmentId = d.Id
        LEFT JOIN Passports p ON e.PassportId = p.Id
        WHERE e.DepartmentId = @DepartmentId;",
                new { DepartmentId = departmentId });
        }

        /// <summary>
        /// Получает информацию о сотруднике по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <returns>Информация о сотруднике.</returns>
        public async Task<Employee> Get(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                e.*, 
                p.*,
                d.*
            FROM Employees e
            LEFT JOIN Passports p ON e.PassportId = p.Id
            LEFT JOIN Departments d ON e.DepartmentId = d.Id
            WHERE e.Id = @Id;";

                var employees = await db.QueryAsync<Employee, Passport, Department, Employee>(
                    query,
                    (employee, passport, department) =>
                    {
                        employee.Passport = passport;
                        employee.Department = department;
                        return employee;
                    },
                    new { Id = id }
                );

                return employees.FirstOrDefault();
            }
        }

        /// <summary>
        /// Создает нового сотрудника.
        /// </summary>
        /// <param name="employee">Информация о сотруднике.</param>
        /// <returns>Созданный сотрудник.</returns>
        public async Task<Employee> Create(Employee employee)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var departmentId = await db.ExecuteScalarAsync<int>(
                    "INSERT INTO Departments (Name, Phone) VALUES (@Name, @Phone) RETURNING Id",
                    employee.Department
                );

                var passportId = await db.ExecuteScalarAsync<int>(
                    "INSERT INTO Passports (Type, Number) VALUES (@Type, @Number) RETURNING Id",
                    employee.Passport
                );

                var sqlQuery = @"
                    INSERT INTO Employees (Name, Surname, Phone, CompanyId, DepartmentId, PassportId)
                    VALUES (@Name, @Surname, @Phone, @CompanyId, @DepartmentId, @PassportId)
                    RETURNING *;
                ";

                return await db.QueryFirstOrDefaultAsync<Employee>(sqlQuery, new
                {
                    employee.Name,
                    employee.Surname,
                    employee.Phone,
                    employee.CompanyId,
                    departmentId,
                    passportId
                });
            }
        }

        /// <summary>
        /// Обновляет информацию о сотруднике.
        /// </summary>
        /// <param name="employee">Обновленная информация о сотруднике.</param>
        /// <returns>Обновленный сотрудник.</returns>
        public async Task<Employee> Update(Employee employee)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                // Обновляем основную информацию о сотруднике
                var updateQuery = @"
                    UPDATE Employees 
                    SET 
                        Name = @Name, 
                        Surname = @Surname, 
                        Phone = @Phone, 
                        CompanyId = @CompanyId
                    WHERE Id = @Id
                    RETURNING *";

                var updatedEmployee = await db.QueryFirstOrDefaultAsync<Employee>(updateQuery, employee);

                // Обновляем Passport
                var updatePassportQuery = @"
                    UPDATE Passports 
                    SET 
                        Type = @Type,
                        Number = @Number
                    WHERE Id = @Id
                    RETURNING *";

                var updatedPassport = await db.QueryFirstOrDefaultAsync<Passport>(updatePassportQuery, employee.Passport);

                // Обновляем Department
                var updateDepartmentQuery = @"
                    UPDATE Departments 
                    SET 
                        Name = @Name,
                        Phone = @Phone
                    WHERE Id = @Id
                    RETURNING *";

                var updatedDepartment = await db.QueryFirstOrDefaultAsync<Department>(updateDepartmentQuery, employee.Department);

                // Обновляем ссылки на Passport и Department в сотруднике
                updatedEmployee.Passport = updatedPassport;
                updatedEmployee.Department = updatedDepartment;

                return updatedEmployee;
            }
        }

        /// <summary>
        /// Удаляет сотрудника по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сотрудника.</param>
        /// <returns>Удаленный сотрудник.</returns>
        public async Task<Employee> Delete(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Employees WHERE Id = @id RETURNING *";
                return await db.QueryFirstOrDefaultAsync<Employee>(sqlQuery, new { id });
            }
        }

        /// <summary>
        /// Выполняет запрос SELECT к базе данных с использованием Dapper, выполняя маппинг результатов на объекты Employee.
        /// </summary>
        /// <param name="query">SQL-запрос SELECT.</param>
        /// <param name="parameters">Параметры запроса.</param>
        /// <returns>Список сотрудников, соответствующих результатам запроса.</returns>
        private async Task<List<Employee>> ExecuteSelectQuery(string query, object parameters = null)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var employees = await db.QueryAsync<Employee, Department, Passport, Employee>(
                    query,
                    (employee, department, passport) =>
                    {
                        // Маппим результаты запроса на объект Employee
                        employee.Department = department;
                        employee.Passport = passport;
                        return employee;
                    },
                    parameters
                );

                return employees.ToList();
            }
        }

    }
}
