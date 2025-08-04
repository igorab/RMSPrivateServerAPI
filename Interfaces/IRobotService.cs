using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotService
{
    Task<robotinfo> Insert(robotinfo robot);
    Task<robotinfo> Update(robotinfo robot);
    Task Delete(Guid id);
    Task<robotinfo> Get(Guid id); 
}
