using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Repository
/// </summary>
public interface IRobotTaskRepository
{
    Task<IEnumerable<robot_task>> GetAll(string robotId);

    Task<robot_task?> Get(string taskId);

    Task<robot_task?> GetCurrent(string robotId);

    Task<string> UpsertAsync(robot_task robot);

    Task<int> DeleteAsync(string id);
}
