using RMSPrivateServerAPI.Entities;

namespace RMSPrivateServerAPI.Interfaces
{
    public interface IPPMRepository
    {
        Task<IEnumerable<PPMTask>> GetAll();

        Task<PPMTask?> Get(int id);
        
        Task<int> UpsertAsync(PPMTask ppmTask);
        
        Task<int> DeleteAsync(int id);
    }
}
