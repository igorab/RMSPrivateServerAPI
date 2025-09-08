using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Repository
/// </summary>
public interface IRobotTaskRepository
{
    Task<IEnumerable<robot_task>> GetAll(Guid robotId);

    Task<robot_task?> GetByTaskId(Guid taskId);

    Task <List<RobotActionsDone?>> GetCurrent(Guid robotId);

    Task<Guid> UpsertAsync(robot_task robot);

    Task<int> DeleteAsync(Guid id);

    /// <summary>
    /// to get available tasks
    /// </summary>
    /// <returns>task list</returns>
    Task<List<TasksDto>> GetAvailableTasks();
}
