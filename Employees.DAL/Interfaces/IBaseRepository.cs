namespace Employees.DAL.Interfaces
{
    /// <summary>
    /// Интерфейс базового репозитория для операций с сущностями.
    /// </summary>
    /// <typeparam name="T">Тип сущности, с которой работает репозиторий.</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Создает новую сущность в репозитории.
        /// </summary>
        /// <param name="entity">Сущность для создания.</param>
        /// <returns>Созданная сущность.</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Получает сущность по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности.</param>
        /// <returns>Сущность, если найдена, иначе - null.</returns>
        Task<T> Get(int id);

        /// <summary>
        /// Возвращает список всех сущностей в репозитории.
        /// </summary>
        /// <returns>Список сущностей.</returns>
        Task<List<T>> Select();

        /// <summary>
        /// Удаляет сущность из репозитория по её идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор сущности для удаления.</param>
        /// <returns>Удаленная сущность, если операция удаления успешно выполнена, иначе - null.</returns>
        Task<T> Delete(int id);

        /// <summary>
        /// Обновляет существующую сущность в репозитории.
        /// </summary>
        /// <param name="entity">Сущность для обновления.</param>
        /// <returns>Обновленная сущность, если операция обновления успешно выполнена, иначе - null.</returns>
        Task<T> Update(T entity);
    }
}
