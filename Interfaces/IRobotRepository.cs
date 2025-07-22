using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotRepository
{
    Task<IEnumerable<robotinfo>> GetAll(bool returnDeletedRecords = false);

    Task<robotinfo?> Get(string id);
    
    Task<string> UpsertAsync(robotinfo robot);
    
    Task<int> DeleteAsync(string id);
}
