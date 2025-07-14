using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotTaskService
{
    Task<RobotTask> Get(int id);
    Task<RobotTask> Insert(RobotTask task);
    Task<RobotTask> Update(RobotTask task);
    Task Delete(int id);    
}
