using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Models;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Service
/// </summary>
public interface IRobotTaskService
{
    Task<robot_task> GetById(string taskId);

    Task <List<RobotTaskFlat?>> GetCurrent(string robotId);

    Task<Queue<RobotAction?>> GetRobotActions(string taskId);

    Task<robot_task> Insert(robot_task task);
    Task<robot_task> Update(robot_task task);
    Task DeleteTask(string id);    
}
