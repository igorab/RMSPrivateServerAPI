using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces
{
    public interface IPPMRepository
    {
        Task<IEnumerable<ppmtask>> GetAll();

        Task<ppmtask?> Get(int id);
        
        Task<int> UpsertAsync(ppmtask ppmTask);
        
        Task<int> DeleteAsync(int id);
    }
}
