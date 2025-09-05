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

    Task <List<RobotActions?>> GetCurrent(Guid robotId);

    Task<Queue<RobotAction>> GetRobotActions(Guid robotId);

    (TasksDto? curTask, List<TaskActionsDto>? taskActions) RobotTaskActions(Guid robotId);

    Task<robot_task> Insert(robot_task task);

    Task<robot_task> Update(robot_task task);

    Task DeleteTask(Guid id);

    Task<bool> UpdateTaskActionStatusToCompleted(Guid taskId, int actionId);

    Task<IEnumerable<robot_task>> GetAll(Guid robotId);

    /// <summary>
    /// Implement this method to assign the task
    /// </summary>    
    Task AssignTaskToRobot(Guid robotId, Guid taskId);

    /// <summary>
    /// set Task status as completed
    /// </summary>    
    Task<bool> TaskStatusDone(Guid RobotTaskId);

    Task<RobotActionsDto>  AddRobotAction(Guid robotId, ActionDoneRequest request);
}
