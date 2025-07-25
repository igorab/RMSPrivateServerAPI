using RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Service
/// </summary>
public interface IRobotTaskService
{
    Task<robot_task> Get(string taskId);

    Task<robot_task> GetCurrent(string robotId);

    Task<robot_task> Insert(robot_task task);
    Task<robot_task> Update(robot_task task);
    Task Delete(string id);    
}
