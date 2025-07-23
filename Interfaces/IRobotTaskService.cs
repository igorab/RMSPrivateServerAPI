using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces;

/// <summary>
/// Robot Task Service
/// </summary>
public interface IRobotTaskService
{
    Task<RobotTask> Get(string robotId);
    Task<RobotTask> Insert(RobotTask task);
    Task<RobotTask> Update(RobotTask task);
    Task Delete(string id);    
}
