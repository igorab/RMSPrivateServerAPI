using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotService
{
    Task<robotinfo> Insert(robotinfo robot);
    Task<robotinfo> Update(robotinfo robot);
    Task Delete(string id);
    Task<robotinfo> Get(string id); 
}
