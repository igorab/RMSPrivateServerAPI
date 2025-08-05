using RMSPrivateServerAPI.Entities;

#pragma warning disable CS1591

namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotService
{
    Task<robot_info> Insert(robot_info robot);
    Task<robot_info> Update(robot_info robot);
    Task Delete(Guid id);
    Task<robot_info?> Get(Guid robotId); 
}
