using RMSPrivateServerAPI.Entities;
#pragma warning disable CS1591
namespace RMSPrivateServerAPI.Interfaces
{
    public interface IPPMRepository
    {
        Task<IEnumerable<ppm_task>> GetAll();

        Task<ppm_task?> Get(int id);
        
        Task<int> UpsertAsync(ppm_task ppmTask);
        
        Task<int> DeleteAsync(int id);
    }
}
