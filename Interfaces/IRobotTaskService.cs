using RMSPrivateServerAPI.DTOs;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Models;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Service
/// </summary>
public interface IRobotTaskService
{
    Task<robot_task> GetById(Guid taskId);

    Task <List<RobotTaskFlat?>> GetCurrent(Guid robotId);

    Task<Queue<RobotAction>> GetRobotActions(Guid robotId);

    (TasksDto? curTask, TaskActionsDto? curAction) RobotTaskActions(Guid robotId);

    Task<robot_task> Insert(robot_task task);

    Task<robot_task> Update(robot_task task);

    Task DeleteTask(Guid id);    
}
