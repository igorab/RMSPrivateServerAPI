using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Роботы
/// </summary>
public interface IRobotRepository
{
    /// <summary>
    /// Все
    /// </summary>
    /// <param name="returnDeletedRecords">удаленные</param>
    /// <returns>Все строки</returns>
    Task<IEnumerable<robot_info>> GetAll(bool returnDeletedRecords = false);

    /// <summary>
    /// Робот
    /// </summary>
    /// <param name="robotId">Id</param>
    /// <returns>Строка</returns>
    Task<robot_info?> Get(Guid robotId);
    
    /// <summary>
    ///  добавить или обновить
    /// </summary>
    /// <param name="robot">Строка</param>
    /// <returns>Guid</returns>
    Task<Guid> UpsertAsync(robot_info robot);
    
    /// <summary>
    /// удалить
    /// </summary>
    /// <param name="id">Guid</param>
    /// <returns>число</returns>
    Task<int> DeleteAsync(Guid id);


    /// <summary>
    /// Робот
    /// </summary>
    /// <param name="hardId">Id</param>
    /// <returns>Строка</returns>
    Task<robot_info?> GetByHardwareId(int hardId);
}
