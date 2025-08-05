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
    /// <returns></returns>
    Task<IEnumerable<robot_info>> GetAll(bool returnDeletedRecords = false);

    /// <summary>
    /// Робот
    /// </summary>
    /// <param name="robotId">Id</param>
    /// <returns></returns>
    Task<robot_info?> Get(Guid robotId);
    
    /// <summary>
    ///  добавить или обновить
    /// </summary>
    /// <param name="robot"></param>
    /// <returns></returns>
    Task<Guid> UpsertAsync(robot_info robot);
    
    /// <summary>
    /// удалить
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> DeleteAsync(Guid id);
}
