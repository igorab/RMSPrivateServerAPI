using RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Repository
/// </summary>
public interface IRobotTaskRepository
{
    Task<IEnumerable<robot_task>> GetAll(string robotId);

    Task<robot_task?> GetByTaskId(string taskId);

    Task <List<RobotTaskFlat?>> GetCurrent(string robotId);

    Task<string> UpsertAsync(robot_task robot);

    Task<int> DeleteAsync(string id);
}
