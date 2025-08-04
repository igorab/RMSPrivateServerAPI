using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotRepository
{
    Task<IEnumerable<robotinfo>> GetAll(bool returnDeletedRecords = false);

    Task<robotinfo?> Get(Guid id);
    
    Task<Guid> UpsertAsync(robotinfo robot);
    
    Task<int> DeleteAsync(Guid id);
}
