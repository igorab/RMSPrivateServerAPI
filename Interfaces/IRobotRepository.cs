using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotRepository
{
    Task<IEnumerable<robotinfo>> GetAll(bool returnDeletedRecords = false);

    Task<robotinfo?> Get(int id);
    
    Task<int> UpsertAsync(robotinfo robot);
    
    Task<int> DeleteAsync(int id);
}
