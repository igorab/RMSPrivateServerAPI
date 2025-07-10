using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces
{
    public interface IRobotRepository
    {
        Task<IEnumerable<Robot>> GetAll(bool returnDeletedRecords = false);

        Task<Robot?> Get(int id);
        
        Task<int> UpsertAsync(Robot robot);
        
        Task<int> DeleteAsync(int id);
    }
}
