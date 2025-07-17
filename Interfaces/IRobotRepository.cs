using RMSPrivateServerAPI.Entities;
namespace RMSPrivateServerAPI.Interfaces;

public interface IRobotRepository
{
    Task<IEnumerable<RobotInfo>> GetAll(bool returnDeletedRecords = false);

    Task<RobotInfo?> Get(int id);
    
    Task<int> UpsertAsync(RobotInfo robot);
    
    Task<int> DeleteAsync(int id);
}
